using AeroVeloz.Application.DTOs.Auth;
using AeroVeloz.Application.DTOs.Users;
using AeroVeloz.Application.Handlers.Result;

namespace AeroVeloz.Application.Contracts.Auth
{
    public interface IAuthenticationServicie
    {
        Task<OperationResult<UserLoginResultDto>> LoginAsync(UserLoginDto dto);
    }
}
