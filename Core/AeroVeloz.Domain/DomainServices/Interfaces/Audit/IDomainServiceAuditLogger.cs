using AeroVeloz.Domain.Entities.Audits;
using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Services.Interfaces.Audits
{
    public interface IDomainServiceAuditLogger
    {
        Task LogOperationalChangeAsync(Guid userId, string entityName, string oldValue, string newValue);
        Task LogUserAccessAsync(Guid userId, string resource, string action);
        Task LogFlightStateChangeAsync(Guid userId, int flightNumber, string oldState, string newState);
        Task LogSystemEventAsync(Guid userId, string eventType, string description);
        Task<IEnumerable<Entities.Audits.Audits>> GetAuditTrailAsync(string entityName, DateTime? from = null, DateTime? to = null);
        Task<IEnumerable<Entities.Audits.Audits>> GetUserAuditTrailAsync(Guid userId, DateTime? from = null, DateTime? to = null);
        bool ValidateAuditIntegrity(Entities.Audits.Audits auditEntry);
        Task<bool> IsAuditRetentionCompliantAsync(DateTime cutoffDate);
    }
}
