using Microsoft.AspNetCore.Mvc;
using ShiftManager.Api.Core;
using ShiftManager.Api.interfaces;

namespace ShiftManager.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var authResponse = await _auth.RegisterAsync(dto);

            return Ok(ApiResponse<AuthResponseDto>.Ok(authResponse, "Owner registered successfully"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var authResponse = await _auth.LoginAsync(dto);

            return Ok(ApiResponse<AuthResponseDto>.Ok(authResponse, "Login successful"));
        }
    }
}