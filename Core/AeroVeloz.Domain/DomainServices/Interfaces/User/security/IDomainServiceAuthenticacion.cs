using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.DomainServices.Interfaces.User.security
{
    public interface IDomainServiceAuthentication {
        Task<ValidationResult> ValidateUserCredentialsAsync(string username, string password);
        Task<bool> IsUserActiveAsync(Guid userId); Task<bool> IsUserLockedAsync(Guid userId);
        Task<bool> HasRolePermissionAsync(Guid userId, string resource, string action); 
        Task<bool> BelongsToOrganizationAsync(Guid userId, int organizationId);
        Task<IEnumerable<string>> GetUserPermissionsAsync(Guid userId); 
        Task<bool> IsAirportAccessAllowedAsync(Guid userId, string airportCode); 
        Task RegisterLoginAttemptAsync(Guid userId, bool successful, string ipAddress); 
        Task<bool> CanAccessFlightAsync(Guid userId, int flightNumber, string airlineCode); 
    }
}
