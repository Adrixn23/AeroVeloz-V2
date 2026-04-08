using System.Collections.Generic;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs;

namespace AeroVeloz.Desktop.Services.Interfaces;

public interface IAdminManagerService
{
    Task<IEnumerable<UserDto>> GetAvailableAdminsAsync();
    Task<bool> AssignAdminToAirportAsync(AssignAdminDto assignAdminDto);
}
