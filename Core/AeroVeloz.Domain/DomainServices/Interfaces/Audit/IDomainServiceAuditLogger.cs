

namespace AeroVeloz.Domain.Services.Interfaces.Audits
{
    public interface IDomainServiceAuditLogger
    {
        Task LogOperationalChangeAsync(Guid userId, string entityName, string oldValue, string newValue);
        Task LogUserAccessAsync(Guid userId, string resource, string action);
        Task LogFlightStateChangeAsync(Guid userId, int flightNumber, string oldState, string newState);
        Task LogSystemEventAsync(Guid userId, string eventType, string description);
        

        //estos eleemento el editor no quiere reconocer el namespace tienen el mismo nombre en clase y namespace pero aunque lo cambie
        //y elimine el registro del namespace como quiera no lo reconoce.
        Task<IEnumerable<Entities.Audits.Audits>> GetAuditTrailAsync(string entityName, DateTime? from = null, DateTime? to = null);
        Task<IEnumerable<Entities.Audits.Audits>> GetUserAuditTrailAsync(Guid userId, DateTime? from = null, DateTime? to = null);
        bool ValidateAuditIntegrity(Entities.Audits.Audits auditEntry);
        Task<bool> IsAuditRetentionCompliantAsync(DateTime cutoffDate);
    }
}
