using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task CreateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task CreateUserAndDoctorAsync(User user, Doctor doctor, UserAudit audit)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();

                if (audit is not null)
                {
                    _context.UsersAudit.Add(audit);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task CreateUserAndPatientAsync(User user, Patient patient, UserAudit audit)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();

                if (audit is not null)
                {
                    _context.UsersAudit.Add(audit);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }


        public async Task UpdateAccountStatusAsync(Guid userId, string newStatus, UserAudit audit)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user is null)
                throw new KeyNotFoundException($"User with id {userId} not found.");

            user.AccountStatus = newStatus;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Update(user);
            if (audit is not null)
            {
                _context.UsersAudit.Add(audit);
                await _context.SaveChangesAsync();
            }   
            await _context.SaveChangesAsync();
        }
    }

}
