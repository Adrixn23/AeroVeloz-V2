using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.Roles;
using AeroVeloz.Domain.Models.Permission;
using AeroVeloz.Domain.Models.Rol;

namespace AeroVeloz.Application.Repositories.Auth
{
    public interface IUserRepositoryAuthorization { 
        //esta interface contiene los elementos que conllevan a la consulta
        //y return de elementos de authrozation segun el usuario y organismo al cual pertenezca el mismo.

        Task<ValidationResult> AuthorizeOrganizationAccessAsync(Guid userId, int orgId);
        Task<RolModel> GetUserRolesAsync(Guid userId, int orgId);
        Task<bool> IsSuperAdminAsync(Guid userId, int orgId);
        Task<IReadOnlyCollection<PermissionModel>> GetUserPermissionsAsync(Guid userId, int orgId);
        Task<bool> IsAirportAdminAsync(Guid userId, int orgId);
        Task<bool> IsAirlineAdminAsync(Guid userId, int orgId);
        Task<ValidationResult> CanModifyFlightAsync(Guid userId, short flightNumber, int orgId);
        Task<ValidationResult> CanModifyUsers(Guid userId, int orgId);
        Task<ValidationResult> CanModifyOrganizations(Guid userId, int orgId) ;
        Task<ValidationResult> CanViewAuditLogsAsync(Guid userId, int orgId);
        Task<bool> HasRoleAsync(Guid userId, int orgId, string rolName);
        Task<ValidationResult> CanModifyOperationsAsync(Guid userId, int orgId);

    }

}
