using AppointmentService.Models;

namespace AppointmentService.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<bool> HasOverlapAsync(Guid doctorId, DateTime start, DateTime end);
        Task AddAsync(Appointment appointment);
        Task<int> GetDailyAppointmentCountAsync(Guid doctorId, DateOnly date);
        Task<int> GetDailyWorkingMinutesAsync(Guid doctorId, DateOnly date);
    }
}
