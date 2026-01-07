using AppointmentService.Models;

namespace AppointmentService.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<bool> HasOverlapAsync(Guid doctorId, DateTime start, DateTime end);
        Task AddAsync(Appointment appointment);
    }
}
