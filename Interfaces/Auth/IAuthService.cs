using ShiftManager.Api.Core;

namespace ShiftManager.Api.interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
    }
}