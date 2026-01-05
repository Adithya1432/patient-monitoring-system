using System.ComponentModel.DataAnnotations;

namespace AppointmentService.Models
{
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public DateTime ScheduledStartTime { get; set; }

        [Required]
        public DateTime ScheduledEndTime { get; set; }

        public DateTime? ActualStartTime { get; set; }

        public DateTime? ActualEndTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string AppointmentType { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = null!;

        public int? CancellationReasonId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<AppointmentHistory> AppointmentHistories { get; set; }
            = new List<AppointmentHistory>();
        public ICollection<AppointmentNote> AppointmentNotes { get; set; }
            = new List<AppointmentNote>();
    }
}