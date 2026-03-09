using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Models.UserSystem;

namespace AeroVeloz.Application.Repositories.Auth
{
    public interface IUserRepositoryAuthenticacion
    {
        Task<ValidationResult> ValidateUserCredentialsAsync(string username, string password, int orgId);
        Task<ValidationResult> IsUserActiveAsync(Guid userId, int orgId);
        Task<ValidationResult> IsUserLockedAsync(Guid userId, int orgId);
        Task<ValidationResult> BelongsToOrganizationAsync(Guid userId, int orgId);
        Task<ValidationResult> IsOrganizationAccessAllowedAsync(int orgId);
        Task<bool> RegisterLoginAttemptAsync(Guid userId, int failedLoginAttempts, DateTime lockedUntil, byte[] ipAddress, int orgId);
        Task<UserSystemModel> GetByUserNameAsync(string nameUser, int orgId);
    }
}
