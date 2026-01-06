using DoctorAvailabilityService.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DoctorAvailabilityService.Data
{
    public class DoctorAvailabilityDbContext : DbContext
    {

        public DoctorAvailabilityDbContext(DbContextOptions<DoctorAvailabilityDbContext> options)
            : base(options)
        {

        }

        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<DoctorLeave> DoctorLeaves { get; set; }
        public DbSet<EmergencyBlock> EmergencyBlocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DoctorSchedule>(entity =>
            {
                entity.HasKey(s => s.ScheduleId);

                entity.Property(s => s.DayOfWeek)
                      .HasMaxLength(10)
                      .IsRequired();

                entity.Property(s => s.StartTime)
                      .IsRequired();

                entity.Property(s => s.EndTime)
                      .IsRequired();

                entity.HasIndex(s => new { s.DoctorId, s.DayOfWeek });
            });


            builder.Entity<DoctorLeave>(entity =>
            {
                entity.HasKey(l => l.LeaveId);

                entity.Property(l => l.Reason)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.HasIndex(l => l.DoctorId);
            });

            builder.Entity<EmergencyBlock>(entity =>
            {
                entity.HasKey(b => b.BlockId);

                entity.Property(b => b.Reason)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(b => b.BlockStartDateTime)
                      .IsRequired();

                entity.Property(b => b.BlockEndDateTime)
                      .IsRequired();

                entity.Property(b => b.CreatedAt)
                      .HasDefaultValueSql("DATEADD(MINUTE, 330, GETUTCDATE())");

                entity.HasIndex(b => new { b.DoctorId, b.BlockStartDateTime, b.BlockEndDateTime });
            });


        }
    }
}