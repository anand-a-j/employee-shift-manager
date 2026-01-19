using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManager.Api.Core;
using ShiftManager.Api.interfaces;

namespace ShiftManager.Api.Controllers
{
    [ApiController]
    [Route("api/staff")]
    [Authorize(Roles = "Owner")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff(CreateStaffDto dto)
        {
            var ownerUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var staffId = await _staffService.CreateStaffAsync(dto, ownerUserId);

            return Ok(ApiResponse<string>.Ok(staffId, "Staff created successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStaff(CreateStaffDto dto)
        {
            var ownerUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var staffId = await _staffService.CreateStaffAsync(dto, ownerUserId);

            return Ok(ApiResponse<string>.Ok(staffId, "Staff created successfully"));
        }
    }
}