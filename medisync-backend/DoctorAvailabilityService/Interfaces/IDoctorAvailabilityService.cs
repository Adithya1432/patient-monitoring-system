namespace DoctorAvailabilityService.Interfaces
{
    public interface IDoctorAvailabilityService
    {
        Task<bool> IsDoctorAvailableAsync(Guid doctorId, DateTime start, DateTime end);
    }
}
