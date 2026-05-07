using AeroVeloz.Web.Models.Auth;

namespace AeroVeloz.Web.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}
