using System.ComponentModel.DataAnnotations;

namespace AppointmentService.Models
{
    public class CancellationReason
    {
        [Key]
        public int ReasonId { get; set; }

        [Required]
        [MaxLength(150)]
        public string ReasonText { get; set; } = null!;
    }
}
