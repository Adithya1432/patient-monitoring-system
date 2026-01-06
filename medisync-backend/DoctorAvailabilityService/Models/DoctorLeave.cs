using System.ComponentModel.DataAnnotations;

namespace DoctorAvailabilityService.Models
{
    public class DoctorLeave
    {
        [Key]
        public Guid LeaveId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Reason { get; set; } = null!;
    }
}
