using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Airports;
using AeroVeloz.Domain.Entities.Users.Roles;

namespace AeroVeloz.Domain.DomainServices.Interfaces.User.Superadmin
{
    public interface IDomainServiceSuperAdmin
    {
        Task<ValidationResult> RegisterAirportAsync(Airport airport);
        Task<ValidationResult> ManageSystemUserAsync(Guid userId, bool activate);
        Task<ValidationResult> AssignRoleToUserAsync(Guid userId, Roles rol, int organizationId);
        Task<ValidationResult> RemoveRoleFromUserAsync(Guid userId, Roles rol, int organizationId);
        Task<ValidationResult> ResetUserPasswordAsync(Guid userId, string newPassword);
        Task<ValidationResult> ManageOrganizationStatusAsync(int OrganizationID, bool isActive);

        /*analizar este elemento puesto que el admin no puede desactivar un usuario del equipo de airport por ejemplo entonces a su vez  si se
        desactiva un airport todos los usuarios deben desactivarse automaticamente, entonces el punto de verificacion es si es mas viable
        tener lo asi o recibir la lista de usuarios o que entonces cuando se consulte tener un metodo en el repositorio que recibe esa lista de
        usuarios y los desactive
        */

        Task<ValidationResult> ManageUserSystemStatusAsync(Guid userId, int OrganizationID, bool isActive);
    
    }
}
