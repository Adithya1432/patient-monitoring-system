using System.ComponentModel.DataAnnotations;

namespace AnalyticsService.Models
{
    public class WaitTimeMetric
    {
        [Key]
        public Guid WaitTimeMetricId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int AverageWaitTimeMinutes { get; set; }
    }
}