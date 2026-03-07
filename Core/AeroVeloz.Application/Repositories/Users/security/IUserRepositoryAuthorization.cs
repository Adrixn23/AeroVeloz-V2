using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.Roles;

namespace AeroVeloz.Application.Repositories.Users.security
{
    public interface IUserRepositoryAuthorization { 
        //esta interface contiene los elementos que conllevan a la consulta
        //y return de elementos de authrozation segun el usuario y organismo al cual pertenezca el mismo.

        Task<ValidationResult> AuthorizeOrganizationAccessAsync(Guid userId, int orgId);
        Task<IReadOnlyCollection<Roles>> GetUserRolesAsync(Guid userId, int orgId);
        Task<bool> IsSuperAdminAsync(Guid userId, int orgId);
        Task<IReadOnlyCollection<Permission>> GetUserPermissionsAsync(Guid userId, int orgId);
        Task<bool> IsAirportAdminAsync(Guid userId, int orgId);
        Task<bool> IsAirlineAdminAsync(Guid userId, int orgId);
        Task<ValidationResult> CanModifyFlightAsync(Guid userId, short flightNumber, int orgId);
        Task<ValidationResult> CanModifyUsers(Guid userId, int orgId);
        Task<ValidationResult> CanModifyOrganizations(Guid userId, int orgId) ;
        Task<ValidationResult> CanViewAuditLogsAsync(Guid userId, int orgId); 


    }

}
