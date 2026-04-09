using AeroVeloz.Desktop.Models.DTOs;

namespace AeroVeloz.Desktop.Services.Interfaces.Auth;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}
