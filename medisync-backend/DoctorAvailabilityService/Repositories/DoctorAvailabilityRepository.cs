using DoctorAvailabilityService.Data;
using DoctorAvailabilityService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabilityService.Repositories
{
    public class DoctorAvailabilityRepository : IDoctorAvailabilityRepository
    {
        private readonly DoctorAvailabilityDbContext _context;

        public DoctorAvailabilityRepository(DoctorAvailabilityDbContext context)
        {
            _context = context;
        }


        public async Task<bool> IsDoctorScheduledAsync(Guid doctorId, string dayOfWeek)
        {
            return await _context.DoctorSchedules.AnyAsync(s =>
                s.DoctorId == doctorId &&
                s.DayOfWeek.ToLower() == dayOfWeek.ToLower()
            );
        }

        public async Task<bool> IsWithinWorkingHoursAsync(
            Guid doctorId,
            string dayOfWeek,
            TimeSpan start,
            TimeSpan end)
        {
            return await _context.DoctorSchedules.AnyAsync(s =>
                s.DoctorId == doctorId &&
                s.DayOfWeek.ToLower() == dayOfWeek.ToLower() &&
                start >= s.StartTime &&
                end <= s.EndTime
            );
        }

        public async Task<bool> IsDoctorOnLeaveAsync(Guid doctorId, DateOnly date)
        {
            return await _context.DoctorLeaves.AnyAsync(l =>
                l.DoctorId == doctorId &&
                date >= l.StartDate &&
                date <= l.EndDate
            );
        }

        public async Task<bool> HasEmergencyBlockAsync(
            Guid doctorId,
            DateTime start,
            DateTime end)
        {
            return await _context.EmergencyBlocks.AnyAsync(b =>
                b.DoctorId == doctorId &&
                start < b.BlockEndDateTime &&
                end > b.BlockStartDateTime
            );
        }
    }
}
