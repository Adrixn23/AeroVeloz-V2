using System.Collections.Generic;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.AdminControl;
using AeroVeloz.Desktop.Models.DTOs.Auth;
using AeroVeloz.Desktop.Models.DTOs.User;

namespace AeroVeloz.Desktop.Services.Interfaces;

public interface IAdminManagerService
{
    /// <summary>
    /// Obtiene la lista de usuarios de la organización del sistema.
    /// </summary>
    Task<IEnumerable<UserDto>> GetAvailableAdminsAsync();

    /// <summary>
    /// Crea un nuevo usuario en el sistema.
    /// </summary>
    Task<bool> CreateUserAsync(CreateUserDto dto);

    /// <summary>
    /// Edita un usuario existente.
    /// </summary>
    Task<bool> UpdateUserAsync(EditUserDto dto);

    /// <summary>
    /// Desactiva un usuario del sistema.
    /// </summary>
    Task<bool> DeactivateUserAsync(Guid userId);
}

