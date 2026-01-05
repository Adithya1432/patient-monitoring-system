using AppointmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Data
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
            : base(options)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }
        public DbSet<AppointmentNote> AppointmentNotes { get; set; }
        public DbSet<CancellationReason> CancellationReasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.AppointmentId);

                entity.Property(a => a.Status)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(a => a.AppointmentType)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(a => a.CreatedAt)
                    .HasDefaultValueSql(
                    "DATEADD(MINUTE, 330, GETUTCDATE())"
                    );
                entity.Property(a => a.UpdatedAt)
                    .HasDefaultValueSql("DATEADD(MINUTE, 330, GETUTCDATE())");

            });

            modelBuilder.Entity<AppointmentHistory>(entity =>
            {
                entity.HasKey(h => h.HistoryId);

                entity.Property(h => h.PreviousStatus)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(h => h.NewStatus)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(h => h.ChangedAt)
                      .HasDefaultValueSql("DATEADD(MINUTE, 330, GETUTCDATE())");

                entity.HasOne(h => h.Appointment)
                      .WithMany(a => a.AppointmentHistories)
                      .HasForeignKey(h => h.AppointmentId)
                      .OnDelete(DeleteBehavior.Cascade); // optional but recommended
            });

            modelBuilder.Entity<AppointmentNote>(entity =>
            {
                entity.HasKey(n => n.NoteId);

                entity.Property(n => n.Note)
                      .IsRequired()
                      .HasColumnType("TEXT");

                entity.Property(n => n.CreatedAt)
                      .HasDefaultValueSql("DATEADD(MINUTE, 330, GETUTCDATE())");

                entity.HasOne(n => n.Appointment)
                      .WithMany(a => a.AppointmentNotes)
                      .HasForeignKey(n => n.AppointmentId)
                      .OnDelete(DeleteBehavior.Cascade); // or Restrict (see note below)

                entity.HasIndex(n => n.AppointmentId);
            });

            modelBuilder.Entity<CancellationReason>(entity =>
            {
                entity.HasKey(r => r.ReasonId);

                entity.Property(r => r.ReasonText)
                      .HasMaxLength(150)
                      .IsRequired();
            });


        }

    }
}
