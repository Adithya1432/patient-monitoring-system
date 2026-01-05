using AppointmentService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentService.Models
{
    public class AppointmentHistory
    {
        [Key]
        public Guid HistoryId { get; set; }

        [Required]
        public Guid AppointmentId { get; set; }

        [Required]
        [MaxLength(20)]
        public string PreviousStatus { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string NewStatus { get; set; } = null!;

        [Required]
        public Guid ChangedBy { get; set; }

        [Required]
        public DateTime ChangedAt { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        public Appointment Appointment { get; set; } = null!;
    }
}