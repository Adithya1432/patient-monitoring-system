using System.ComponentModel.DataAnnotations;

namespace AnalyticsService.Models
{
    public class PeakHour
    {
        [Key]
        public Guid PeakHourId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        [Range(0, 23)]
        public int HourOfDay { get; set; }

        [Required]
        public int AppointmentCount { get; set; }
    }
}