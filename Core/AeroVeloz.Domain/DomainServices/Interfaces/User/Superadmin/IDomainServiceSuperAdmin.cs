using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Airports;
using AeroVeloz.Domain.Entities.Users;
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
        Task<ValidationResult> MagageUserSystemStatusAsync(Guid userId, int OrganizationID, bool isActive);
        Task<IEnumerable<Airport>> GetAllAirportsAsync();
        Task<IEnumerable<Airline>> GetAllAirlinesAsync();
        Task<IEnumerable<Domain.Entities.Users.User>> GetUsersSystemAsync();
        Task<ValidationResult> ViewSystemAuditAsync(Guid userId, DateTime? from, DateTime? to);
        Task<IEnumerable<AeroVeloz.Domain.Entities.Users.User>> GetAllSystemOrganizationsAsync();

    }
}
