using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Models.Permission;
using AeroVeloz.Domain.Models.Rol;

namespace AeroVeloz.Application.Repositories.Auth
{
    public interface IUserRepositoryAuthorization
    {
        Task<ValidationResult> AuthorizeOrganizationAccessAsync(Guid userId, int orgId);
        Task<RolModel> GetUserRolesAsync(Guid userId, int orgId);
        Task<IReadOnlyCollection<PermissionModel>> GetUserPermissionsAsync(Guid userId, int orgId);
        Task<bool> IsAirlineAdminAsync(Guid userId, int orgId);
        Task<ValidationResult> CanModifyFlightAsync(Guid userId, short flightNumber, int orgId);
        Task<bool> HasRoleAsync(Guid userId, int orgId, string rolName);
    }
}
