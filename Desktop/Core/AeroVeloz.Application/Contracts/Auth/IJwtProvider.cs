using AeroVeloz.Application.DTOs.Auth;

namespace AeroVeloz.Application.Contracts.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(UserLoginResultDto user);
    }
}
