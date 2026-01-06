using AnalyticsService.Models;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Data
{
    public class AnalyticsDbContext : DbContext
    {
        public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options)
            : base(options)
        {

        }

        public DbSet<DoctorUtilization> DoctorUtilizations { get; set; }
        public DbSet<NoShowStatistic> NoShowStatistics { get; set; }
        public DbSet<WaitTimeMetric> WaitTimeMetrics { get; set; }
        public DbSet<PeakHour> PeakHours { get; set; }
        public DbSet<AppointmentSummary> AppointmentSummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DoctorUtilization>(entity =>
            {
                entity.HasKey(u => u.DoctorUtilizationId);

                entity.Property(u => u.UtilizationPercentage)
                      .HasPrecision(5, 2)
                      .IsRequired();

                entity.Property(u => u.Date)
                      .IsRequired();

                entity.HasIndex(u => new { u.DoctorId, u.Date })
                      .IsUnique();
            });

            builder.Entity<NoShowStatistic>(entity =>
            {
                entity.HasKey(n => n.NoShowStatisticId);

                entity.Property(n => n.NoShowRate)
                      .HasPrecision(5, 2)
                      .IsRequired();

                entity.Property(n => n.Date)
                      .IsRequired();

                entity.HasIndex(n => n.Date)
                      .IsUnique();
            });

            builder.Entity<WaitTimeMetric>(entity =>
            {
                entity.HasKey(w => w.WaitTimeMetricId);

                entity.Property(w => w.AverageWaitTimeMinutes)
                      .IsRequired();

                entity.Property(w => w.Date)
                      .IsRequired();

                entity.HasIndex(w => w.Date)
                      .IsUnique();
            });

            builder.Entity<PeakHour>(entity =>
            {
                entity.HasKey(p => p.PeakHourId);

                entity.Property(p => p.Date)
                      .IsRequired();

                entity.Property(p => p.HourOfDay)
                      .IsRequired();

                entity.Property(p => p.AppointmentCount)
                      .IsRequired();

                entity.HasIndex(p => new { p.Date, p.HourOfDay })
                      .IsUnique();
            });

            builder.Entity<AppointmentSummary>(entity =>
            {
                entity.HasKey(s => s.AppointmentSummaryId);

                entity.Property(s => s.Date)
                      .IsRequired();

                entity.Property(s => s.TotalAppointments)
                      .IsRequired();

                entity.Property(s => s.CompletedCount)
                      .IsRequired();

                entity.Property(s => s.CancelledCount)
                      .IsRequired();

                entity.Property(s => s.NoShowCount)
                      .IsRequired();

                entity.HasIndex(s => s.Date)
                      .IsUnique();
            });


        }
    }
}
