using System.ComponentModel.DataAnnotations;

namespace AnalyticsService.Models
{
    public class AppointmentSummary
    {
        [Key]
        public Guid AppointmentSummaryId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int TotalAppointments { get; set; }

        [Required]
        public int CompletedCount { get; set; }

        [Required]
        public int CancelledCount { get; set; }

        [Required]
        public int NoShowCount { get; set; }
    }
}