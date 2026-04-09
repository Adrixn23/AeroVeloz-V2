using AeroVeloz.Desktop.Models.DTOs.Audit;

namespace AeroVeloz.Desktop.Services.Interfaces.Audit;

public interface IAuditService
{
    Task<IEnumerable<AuditDto>> GetUserAuditAsync(Guid targetUserId);


    Task<IEnumerable<AuditDto>> GetGlobalAuditAsync(int orgId);
}
