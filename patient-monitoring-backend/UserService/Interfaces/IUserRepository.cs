using UserService.Models;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task CreateUserAsync(User user);
        Task CreatePatientAsync(Patient patient);
        Task CreateDoctorAsync(Doctor doctor);
        Task CreateUserAndDoctorAsync(User user, Doctor doctor, UserAudit audit);
        Task CreateUserAndPatientAsync(User user, Patient patient, UserAudit audit);
        Task<User?> GetByIdAsync(Guid userId);
        Task UpdateAccountStatusAsync(Guid userId, string newStatus, UserAudit audit);
    }

}
