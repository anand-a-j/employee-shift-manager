using Microsoft.EntityFrameworkCore;
using ShiftManager.Api.Core;
using ShiftManager.Api.Data;
using ShiftManager.Api.Entities;
using ShiftManager.Api.interfaces;

namespace ShiftManager.Api.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly JwtHelper _jwt;

        public AuthService(AppDbContext db, JwtHelper jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
                throw new AppException("Email already registered", System.Net.HttpStatusCode.BadRequest);

            var business = new Business
            {
                Name = dto.BusinessName
            };

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = UserRole.Owner,
                BusinessId = business.Id,
            };

            _db.Businesses.Add(business);
            _db.Users.Add(user);

            await _db.SaveChangesAsync();

            var userResponseDto = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,
            };

            var authResponseDto = new AuthResponseDto
            {
                Token = _jwt.GenerateToken(user),
                User = userResponseDto
            };

            return authResponseDto;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                throw new AppException("Invalid email or password", System.Net.HttpStatusCode.Unauthorized);

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new AppException("Invalid email or password", System.Net.HttpStatusCode.Unauthorized);

            var userResponse = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                businessId = user.BusinessId.ToString(),
            };

            return new AuthResponseDto
            {
                Token = _jwt.GenerateToken(user),
                User = userResponse,
            };
        }
    }
}