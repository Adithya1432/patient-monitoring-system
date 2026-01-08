using AppointmentService.DTOs;
using AppointmentService.Interfaces;
using AppointmentService.Models;
using Shared.Protos.DoctorAvailability;
using Shared.Protos.Optimization;
using Shared.Protos.User;

namespace AppointmentService.Services
{
    public class AppointmentsService : IAppointmentService
    {
        private readonly DoctorUserService.DoctorUserServiceClient _userClient;
        private readonly DoctorAvailabilityCheckService.DoctorAvailabilityCheckServiceClient _doctorAvailabilityClient;
        private readonly OptimizationService.OptimizationServiceClient _optimizationClient;
        private readonly IAppointmentRepository _repository;

        public AppointmentsService(
            DoctorUserService.DoctorUserServiceClient userClient,
            DoctorAvailabilityCheckService.DoctorAvailabilityCheckServiceClient docotrAvailabilityClient,
            OptimizationService.OptimizationServiceClient optimizationClient,
            IAppointmentRepository repository)
        {
            _userClient = userClient;
            _doctorAvailabilityClient = docotrAvailabilityClient;
            _optimizationClient = optimizationClient;
            _repository = repository;
        }

        public async Task<BookAppointmentResponseDto> BookAsync(
            BookAppointmentRequestDto request)
        {
            // 🔹 Get optimization rules
            var rulesResponse = await _optimizationClient.GetActiveRulesAsync(
                new EmptyRequest());

            var config = MapRules(rulesResponse);

            var start = request.PreferredDate.ToDateTime(
                TimeOnly.FromTimeSpan(request.PreferredTime));

            if (start < DateTime.Now.AddHours(config.MinHoursBeforeBooking))
                throw new Exception("Booking too soon");

            if (start > DateTime.Now.AddDays(config.MaxDaysInAdvanceBooking))
                throw new Exception("Booking too far in advance");

            var end = start.AddMinutes(config.DefaultAppointmentDurationMinutes);

            var doctors = await _userClient.GetDoctorsBySpecialityAsync(
                new Shared.Protos.User.SpecialityRequest
                {
                    Speciality = request.Speciality
                });

            if (!doctors.Doctors.Any())
                throw new Exception("No doctors available");

            foreach (var doctor in doctors.Doctors)
            {
                var doctorId = Guid.Parse(doctor.DoctorId);

                var availability = await _doctorAvailabilityClient.IsDoctorAvailableAsync(
                    new Shared.Protos.DoctorAvailability.DoctorAvailabilityRequest
                    {
                        DoctorId = doctor.DoctorId,
                        StartTime = start.ToString("O"),
                        EndTime = end.ToString("O")
                    });

                if (!availability.IsAvailable)
                    continue;

                if (await _repository.HasOverlapAsync(doctorId, start, end))
                    continue;

                if (await _repository.GetDailyAppointmentCountAsync(
                        doctorId, request.PreferredDate) >=
                    config.MaxAppointmentsPerDay)
                    continue;

                if (await _repository.GetDailyWorkingMinutesAsync(
                        doctorId, request.PreferredDate) +
                    config.DefaultAppointmentDurationMinutes >
                    config.MaxDailyWorkingHours * 60)
                    continue;

                var appointment = new Appointment
                {
                    AppointmentId = Guid.NewGuid(),
                    PatientId = request.PatientId,
                    DoctorId = doctorId,
                    ScheduledStartTime = start,
                    ScheduledEndTime = end,
                    AppointmentType = "Consultation",
                    Status = "Scheduled",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _repository.AddAsync(appointment);

                return new BookAppointmentResponseDto
                {
                    AppointmentId = appointment.AppointmentId,
                    DoctorId = appointment.DoctorId,
                    ScheduledStartTime = start,
                    ScheduledEndTime = end,
                    Status = appointment.Status
                };
            }

            throw new Exception("No available slot for the preferred time");
        }

        private BookingOptimizationConfig MapRules(
            OptimizationRulesResponse response)
        {
            var dict = response.Rules
                .ToDictionary(r => r.RuleName, r => r.RuleValue);

            int GetInt(string key, int def)
                => dict.ContainsKey(key) ? int.Parse(dict[key]) : def;

            string GetString(string key, string def)
                => dict.ContainsKey(key) ? dict[key] : def;

            return new BookingOptimizationConfig
            {
                DefaultAppointmentDurationMinutes =
                    GetInt("DEFAULT_APPOINTMENT_DURATION_MINUTES", 30),
                AppointmentBufferMinutes =
                    GetInt("APPOINTMENT_BUFFER_MINUTES", 0),
                MaxAppointmentsPerDay =
                    GetInt("MAX_APPOINTMENTS_PER_DAY", int.MaxValue),
                MaxDailyWorkingHours =
                    GetInt("MAX_DAILY_WORKING_HOURS", int.MaxValue),
                MinHoursBeforeBooking =
                    GetInt("MIN_HOURS_BEFORE_BOOKING", 0),
                MaxDaysInAdvanceBooking =
                    GetInt("MAX_DAYS_IN_ADVANCE_BOOKING", int.MaxValue),
                DoctorAssignmentStrategy =
                    GetString("DOCTOR_ASSIGNMENT_STRATEGY", "LEAST_WORKLOAD")
            };
        }
    }
}