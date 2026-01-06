using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalyticsService.Models
{
    public class NoShowStatistic
    {
        [Key]
        public Guid NoShowStatisticId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal NoShowRate { get; set; }
    }
}