using System.ComponentModel.DataAnnotations;

namespace DoctorAvailabilityService.Models
{
    public class DoctorSchedule
    {
        [Key]
        public Guid ScheduleId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        [MaxLength(10)]
        public string DayOfWeek { get; set; } = null!;

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }
    }

}