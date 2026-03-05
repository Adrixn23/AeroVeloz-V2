using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.DomainServices.Interfaces.User.security
{
    public interface IUserRepositoryAuthenticacion
    {
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
        Task<bool> IsUserActiveAsync(Guid userId);
        Task<bool> IsUserLockedAsync(Guid userId);
        Task<bool> BelongsToOrganizationAsync(Guid userId, int organizationId);
        Task<IEnumerable<string>> GetUserPermissionsAsync(Guid userId); 
        Task<bool> IsAirportAccessAllowedAsync(Guid userId, string airportCode);
        Task RegisterLoginAttemptAsync(Guid userId, int failedLoginAttempts, DateTime lockedUntil, string ipAddress);
        

    }
}
