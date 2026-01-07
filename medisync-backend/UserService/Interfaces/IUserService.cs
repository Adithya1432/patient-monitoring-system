using UserService.DTOs;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        Task PatientSignupAsync(PatientSignupDto dto);
        Task DoctorSignupAsync(DoctorSignupDto dto);
        Task ActivateUserAsync(Guid userId);
        Task<List<Guid>> GetDoctorsBySpecialityAsync(string speciality);
    }
}
