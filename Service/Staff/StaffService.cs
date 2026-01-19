using Microsoft.EntityFrameworkCore;
using ShiftManager.Api.Core;
using ShiftManager.Api.Data;
using ShiftManager.Api.Entities;
using ShiftManager.Api.interfaces;

namespace ShiftManager.Api.Service
{
    public class StaffService : IStaffService
    {
        private readonly AppDbContext _db;

        public StaffService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<string> CreateStaffAsync(CreateStaffDto dto, Guid ownerUserId)
        {
            var owner = await _db.Users.FirstOrDefaultAsync(
                x => x.Id == ownerUserId && x.Role == UserRole.Owner
            );

            if (owner == null)
                throw new AppException("Unauthorized", System.Net.HttpStatusCode.Unauthorized);

            var existingUser = await _db.Users.AnyAsync(x => x.Email == dto.Email);

            if (existingUser)
                throw new AppException("Email already exists", System.Net.HttpStatusCode.BadRequest);

            var staff = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = UserRole.Shaff,
                BusinessId = owner.BusinessId,
            };

            _db.Users.Add(staff);
            await _db.SaveChangesAsync();

            return staff.Id.ToString();
        }

        public async Task<StaffResponseDto> UpdateStaffAsync(Guid staffId, UpdateStaffDto dto, Guid ownerUserId)
        {
            var owner = await _db.Users.FirstOrDefaultAsync(
                      x => x.Id == ownerUserId && x.Role == UserRole.Owner
                   );

            if (owner == null)
                throw new AppException("Unauthorized", System.Net.HttpStatusCode.Unauthorized);

            var staff = await _db.Users.FirstOrDefaultAsync(
                x => x.Id == staffId && x.Role == UserRole.Shaff && x.BusinessId == ownerUserId
            );

            if (staff == null)
                throw new AppException("Staff not found", System.Net.HttpStatusCode.NotFound);

            staff.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                staff.Email = dto.Email;

            await _db.SaveChangesAsync();

            return new StaffResponseDto
            {
                Id = staff.Id,
                Name = staff.Name,
                Email = staff.Email,
                Role = staff.Role,
            };
        }
    }
}