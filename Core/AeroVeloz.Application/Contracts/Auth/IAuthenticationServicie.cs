using AeroVeloz.Application.DTOs.Auth;
using AeroVeloz.Application.DTOs.Users;
using AeroVeloz.Application.Services.Result;

namespace AeroVeloz.Application.Contracts.Auth
{
    public interface IAuthenticationService
    {
        Task<OperationResult<UserLoginResultDto>> LoginAsync(UserLoginDto dto);
    }
}
