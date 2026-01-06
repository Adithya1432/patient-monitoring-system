using Microsoft.EntityFrameworkCore;
using NotificationService.Models;

namespace NotificationService.Data
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationRetry> NotificationRetries { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.NotificationId);

                entity.Property(n => n.Channel)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(n => n.Type)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(n => n.Status)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(n => n.SentAt)
                      .IsRequired(false);

                entity.HasIndex(n => n.AppointmentId);
                entity.HasIndex(n => n.RecipientId);
                entity.HasIndex(n => n.Status);
            });

            builder.Entity<NotificationTemplate>(entity =>
            {
                entity.HasKey(t => t.TemplateId);

                entity.Property(t => t.TemplateName)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(t => t.Channel)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(t => t.Content)
                      .HasColumnType("TEXT")
                      .IsRequired();

                entity.HasIndex(t => new { t.TemplateName, t.Channel })
                      .IsUnique();
            });

            builder.Entity<NotificationRetry>(entity =>
            {
                entity.HasKey(r => r.RetryId);

                entity.Property(r => r.RetryCount)
                      .IsRequired();

                entity.Property(r => r.NextRetryAt)
                      .IsRequired();

                entity.HasOne(r => r.Notification)
                      .WithMany(n => n.NotificationRetries)
                      .HasForeignKey(r => r.NotificationId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(r => new { r.NotificationId, r.RetryCount });
            });

        }

    }
}
