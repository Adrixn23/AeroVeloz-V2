using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.Roles;

namespace AeroVeloz.Domain.DomainServices.Interfaces.User
{
    public interface IDomainServiceUser
    {
        Task<ValidationResult> ValidateRoleAssignment(Domain.Entities.Users.User user, Roles role, int orgId);
        Task<ValidationResult> ValidateUserActivation(Domain.Entities.Users.User user);
        Task<ValidationResult> ValidateUserDeactivation(Domain.Entities.Users.User user);
        Task<ValidationResult> ValidatePermissionRoleAssignment(Domain.Entities.Users.User user, Roles role, Permission permission);
        Task<ValidationResult> ValidateOrganizationAssignment(Domain.Entities.Users.User user, int orgId);
    }
}
