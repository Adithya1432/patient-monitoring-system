using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    public class Doctor
    {
        [Key]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; } 

        [Required]
        [MaxLength(50)]
        public string RegistrationNumber { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Speciality { get; set; } = null!;

        public int YearsOfExperience { get; set; }

        [Required]
        [MaxLength(20)]
        public string ConsultationType { get; set; } = null!;
        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; } = null!;
        public User User { get; set; } = null!;

    }
}