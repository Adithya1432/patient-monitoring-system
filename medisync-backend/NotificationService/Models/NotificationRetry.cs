using NotificationService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models
{
    public class NotificationRetry
    {
        [Key]
        public Guid RetryId { get; set; }

        [Required]
        public Guid NotificationId { get; set; }

        [Required]
        public int RetryCount { get; set; }

        [Required]
        public DateTime NextRetryAt { get; set; }

        [ForeignKey(nameof(NotificationId))]
        public Notification Notification { get; set; } = null!;
    }
}