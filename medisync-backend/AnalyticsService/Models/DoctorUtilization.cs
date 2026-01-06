using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalyticsService.Models
{
    public class DoctorUtilization
    {
        [Key]
        public Guid DoctorUtilizationId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal UtilizationPercentage { get; set; }
    }
}
