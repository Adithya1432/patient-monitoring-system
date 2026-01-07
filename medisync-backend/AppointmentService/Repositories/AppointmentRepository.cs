using AppointmentService.Data;
using AppointmentService.Interfaces;
using AppointmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentDbContext _context;

        public AppointmentRepository(AppointmentDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasOverlapAsync(
            Guid doctorId,
            DateTime start,
            DateTime end)
        {
            return await _context.Appointments.AnyAsync(a =>
                a.DoctorId == doctorId &&
                a.Status == "Scheduled" &&
                start < a.ScheduledEndTime &&
                end > a.ScheduledStartTime
            );
        }

        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }
    }

}
