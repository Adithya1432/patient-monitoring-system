using Azure.Core;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using UserService.DTOs;
using UserService.Helpers;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Services
{
    public class UsersService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IUserRepository repository, ILogger<UsersService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task PatientSignupAsync(PatientSignupDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Name)
                || string.IsNullOrWhiteSpace(dto.Email)
                || string.IsNullOrWhiteSpace(dto.Phone)
                || string.IsNullOrWhiteSpace(dto.Password)
                || string.IsNullOrWhiteSpace(dto.Gender)
                || dto.DateOfBirth == default)
            {
                throw new InvalidOperationException("Incomplete signup payload. All user and patient fields are required.");
            }

            try
            {
                if (await _repository.EmailExistsAsync(dto.Email))
                    throw new InvalidOperationException("Email already exists");

                var userId = Guid.NewGuid();

                var user = new User
                {
                    UserId = userId,
                    FullName = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    PasswordHash = PasswordHasher.Hash(dto.Password),
                    Role = "Patient",
                    AccountStatus = "Active",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var patient = new Patient
                {
                    UserId = userId,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender
                };


                var audit = new UserAudit
                {
                    AuditId = Guid.NewGuid(),
                    UserId = userId,
                    Action = "CreatePatient",
                    PerformedBy = "Patient",
                    Timestamp = DateTime.Now,
                    Remarks = "Patient signup"
                };

                await _repository.CreateUserAndPatientAsync(user, patient, audit);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during patient signup for email {Email}", dto.Email);
                throw;
            }
        }

        public async Task DoctorSignupAsync(DoctorSignupDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Name)
                || string.IsNullOrWhiteSpace(dto.Email)
                || string.IsNullOrWhiteSpace(dto.Phone)
                || string.IsNullOrWhiteSpace(dto.Password)
                || string.IsNullOrWhiteSpace(dto.RegistrationNumber)
                || string.IsNullOrWhiteSpace(dto.Speciality)
                || string.IsNullOrWhiteSpace(dto.ConsultationType))
            {
                throw new InvalidOperationException("Incomplete signup payload. All user and doctor fields are required.");
            }

            try
            {
                if (await _repository.EmailExistsAsync(dto.Email))
                    throw new InvalidOperationException("Email already exists");

                var userId = Guid.NewGuid();

                var user = new User
                {
                    UserId = userId,
                    FullName = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    PasswordHash = PasswordHasher.Hash(dto.Password),
                    Role = "Doctor",
                    AccountStatus = "Pending Approval",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var doctor = new Doctor
                {
                    UserId = userId,
                    RegistrationNumber = dto.RegistrationNumber,
                    Speciality = dto.Speciality,
                    YearsOfExperience = dto.YearsOfExperience,
                    ConsultationType = dto.ConsultationType,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender
                };

                
                var audit = new UserAudit
                {
                    AuditId = Guid.NewGuid(),
                    UserId = userId,
                    Action = "CreateDoctor",
                    PerformedBy = "Doctor",
                    Timestamp = DateTime.Now,
                    Remarks = "Doctor signup (pending approval)"
                };

                await _repository.CreateUserAndDoctorAsync(user, doctor, audit);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during doctor signup for email {Email}", dto.Email);
                throw;
            }
        }

        public async Task ActivateUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid user id", nameof(userId));

            try
            {
                var user = await _repository.GetByIdAsync(userId);
                if (user is null)
                    throw new KeyNotFoundException("User not found.");

                if (string.Equals(user.AccountStatus, "Active", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("Account is already active.");

                if (!string.Equals(user.AccountStatus, "Pending Approval", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException($"Cannot activate account from status '{user.AccountStatus}'.");
                
               
                var audit = new UserAudit
                {
                    AuditId = Guid.NewGuid(),
                    UserId = userId,
                    Action = "CreateDoctor",
                    PerformedBy = "Admin",
                    Timestamp = DateTime.Now,
                    Remarks = "Doctor Account activated"
                };
                await _repository.UpdateAccountStatusAsync(userId, "Active", audit);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while activating user {UserId}", userId);
                throw;
            }
        }

        public Task<List<Guid>> GetDoctorsBySpecialityAsync(string speciality)
        => _repository.GetDoctorsBySpecialityAsync(speciality);
    }
}

