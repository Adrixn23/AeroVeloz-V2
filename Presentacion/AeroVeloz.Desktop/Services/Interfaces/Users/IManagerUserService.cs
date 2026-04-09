
using AeroVeloz.Desktop.Models.DTOs.User;

namespace AeroVeloz.Desktop.Services.Interfaces.Users;

public interface IManagerUserService
{
    Task<IEnumerable<UserDto>> GetAirportUsersAsync();
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task<bool> CreateUserAsync(CreateUserDto user);
    Task<bool> UpdateUserAsync(Guid userId, EditUserDto user);
    Task<bool> DeleteUserAsync(Guid userId);
}

