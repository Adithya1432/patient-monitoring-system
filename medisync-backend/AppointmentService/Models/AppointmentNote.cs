using AppointmentService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentService.Models
{
    public class AppointmentNote
    {
        [Key]
        public Guid NoteId { get; set; }

        [Required]
        public Guid AppointmentId { get; set; }

        [Required]
        public string Note { get; set; } = null!;

        [Required]
        public Guid CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        public Appointment Appointment { get; set; } = null!;
    }
}