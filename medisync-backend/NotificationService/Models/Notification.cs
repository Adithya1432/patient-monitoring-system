using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }

        [Required]
        public Guid AppointmentId { get; set; }

        [Required]
        public Guid RecipientId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Channel { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = null!;
        public DateTime? SentAt { get; set; }
        public ICollection<NotificationRetry> NotificationRetries { get; set; }
            = new List<NotificationRetry>();
    }
}