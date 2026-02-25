using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.DomainServices.Interfaces.User.Superadmin
{
    public interface IDomainServiceSuperAdmin
    {
        Task<ValidationResult> CreateAirportAdminAsync(string airportCode, string username, string password);
        Task<ValidationResult> ManageSystemUserAsync(Guid userId, bool activate);
        Task<IEnumerable<AeroVeloz.Domain.Entities.Users.User>> GetAllSystemUsersAsync();
        Task<ValidationResult> AssignRoleToUserAsync(Guid userId, short roleId, int organizationId);
        Task<ValidationResult> RemoveRoleFromUserAsync(Guid userId, short roleId, int organizationId);
        Task<bool> CanPerformSystemMaintenanceAsync(Guid userId);
        Task<ValidationResult> ResetUserPasswordAsync(Guid userId, string newPassword);
        Task<ValidationResult> ManageAirportStatusAsync(string airportCode, bool activate);
        Task<IEnumerable<AeroVeloz.Domain.Entities.Airports.Airport>> GetAllAirportsAsync();
        Task<ValidationResult> CreateSystemBackupAsync(Guid requestingUserId);
        Task<ValidationResult> ViewSystemAuditAsync(Guid userId, DateTime? from, DateTime? to);
        Task<bool> IsSuperAdminActionAuthorizedAsync(Guid userId, string action);
        Task<ValidationResult> ManageOrganizationAsync(string code, string type, bool create);
    }
}
