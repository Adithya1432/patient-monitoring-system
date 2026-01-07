using AppointmentService.DTOs;
using AppointmentService.Interfaces;
using AppointmentService.Models;
using Shared.Protos.DoctorAvailability;
using Shared.Protos.User;

namespace AppointmentService.Services
{
    public class AppointmentsService : IAppointmentService
    {
        private readonly DoctorUserService.DoctorUserServiceClient _userClient;
        private readonly DoctorAvailabilityCheckService.DoctorAvailabilityCheckServiceClient _availabilityClient;
        private readonly IAppointmentRepository _repository;

        public AppointmentsService(
            DoctorUserService.DoctorUserServiceClient userClient,
            DoctorAvailabilityCheckService.DoctorAvailabilityCheckServiceClient availabilityClient,
            IAppointmentRepository repository)
        {
            _userClient = userClient;
            _availabilityClient = availabilityClient;
            _repository = repository;
        }

        public async Task<BookAppointmentResponseDto> BookAsync(
            BookAppointmentRequestDto request)
        {
            var start = request.PreferredDate.ToDateTime(TimeOnly.FromTimeSpan(request.PreferredTime));

            var end = start.AddMinutes(30);


            // 1️⃣ Get doctors by speciality
            var doctors = await _userClient.GetDoctorsBySpecialityAsync(
                new Shared.Protos.User.SpecialityRequest
                {
                    Speciality = request.Speciality
                });

            if (!doctors.Doctors.Any())
                throw new Exception("No doctors available");

            // 2️⃣ Try each doctor
            foreach (var doctor in doctors.Doctors)
            {
                var doctorId = Guid.Parse(doctor.DoctorId);

                // 2a️⃣ Check availability service
                var availability = await _availabilityClient.IsDoctorAvailableAsync(
                    new Shared.Protos.DoctorAvailability.DoctorAvailabilityRequest
                    {
                        DoctorId = doctor.DoctorId,
                        StartTime = start.ToString("O"),
                        EndTime = end.ToString("O")
                    });

                if (!availability.IsAvailable)
                    continue;

                // 2b️⃣ Check appointment overlap (OWN DB)
                var hasOverlap = await _repository.HasOverlapAsync(
                    doctorId, start, end);

                if (hasOverlap)
                    continue;

                // 3️⃣ Book appointment
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
    
    }
}