namespace DoctorAvailabilityService.Interfaces
{
    public interface IDoctorAvailabilityRepository
    {
        Task<bool> IsDoctorScheduledAsync(Guid doctorId, string dayOfWeek);
        Task<bool> IsWithinWorkingHoursAsync(Guid doctorId, string dayOfWeek, TimeSpan start, TimeSpan end);
        Task<bool> IsDoctorOnLeaveAsync(Guid doctorId, DateOnly date);
        Task<bool> HasEmergencyBlockAsync(Guid doctorId, DateTime start, DateTime end);
    }
}
