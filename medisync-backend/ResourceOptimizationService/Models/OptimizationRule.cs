using System.ComponentModel.DataAnnotations;

namespace ResourceOptimizationService.Models
{
    public class OptimizationRule
    {
        [Key]
        public Guid RuleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RuleName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string RuleValue { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }
    }
}