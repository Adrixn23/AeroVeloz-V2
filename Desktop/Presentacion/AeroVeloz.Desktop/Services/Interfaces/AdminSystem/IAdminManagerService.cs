
using AeroVeloz.Desktop.Models.DTOs.Auth;
using AeroVeloz.Desktop.Models.DTOs.User;
using AuthUserDto = AeroVeloz.Desktop.Models.DTOs.Auth.UserDto;

namespace AeroVeloz.Desktop.Services.Interfaces.AdminSystem;

public interface IAdminManagerService
{
    Task<IEnumerable<AuthUserDto>> GetAvailableAdminsAsync();

  
    Task<bool> CreateUserAsync(CreateUserDto dto);

   
    Task<bool> UpdateUserAsync(EditUserDto dto);

    Task<bool> DeactivateUserAsync(Guid userId);
}



