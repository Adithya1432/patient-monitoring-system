using DoctorAvailabilityService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabilityService.Services
{
    public class DoctorAvailableService:IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _repository;

        public DoctorAvailableService(IDoctorAvailabilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsDoctorAvailableAsync(
        Guid doctorId,
        DateTime start,
        DateTime end)
        {
            var dayName = start.DayOfWeek.ToString();
            var date = DateOnly.FromDateTime(start);

            if (!await _repository.IsDoctorScheduledAsync(doctorId, dayName))
                return false;

            if (!await _repository.IsWithinWorkingHoursAsync(
                    doctorId,
                    dayName,
                    start.TimeOfDay,
                    end.TimeOfDay))
                return false;

            if (await _repository.IsDoctorOnLeaveAsync(doctorId, date))
                return false;

            if (await _repository.HasEmergencyBlockAsync(doctorId, start, end))
                return false;

            return true;
        }

    }
}
