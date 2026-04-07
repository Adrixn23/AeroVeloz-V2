using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs;

namespace AeroVeloz.Desktop.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}
