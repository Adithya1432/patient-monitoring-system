using System.ComponentModel.DataAnnotations;

namespace DoctorAvailabilityService.Models
{
    public class EmergencyBlock
    {
        [Key]
        public Guid BlockId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public DateTime BlockStartDateTime { get; set; }

        [Required]
        public DateTime BlockEndDateTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string Reason { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}