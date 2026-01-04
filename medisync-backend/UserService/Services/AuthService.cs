using UserService.DTOs;
using UserService.Helpers;
using UserService.Interfaces;

namespace UserService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return null;

            var isValid = PasswordHasher.Verify(
                request.Password,
                user.PasswordHash
            );

            if (!isValid)
                return null;

            return new LoginResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Role = user.Role
            };
        }

    }

}
