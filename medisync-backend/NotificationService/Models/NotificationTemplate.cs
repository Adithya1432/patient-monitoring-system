using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class NotificationTemplate
    {
        [Key]
        public Guid TemplateId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TemplateName { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Channel { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;
    }
}