using AeroVeloz.Transversal.Monitoring;

namespace AeroVeloz.Transversal.Contracts.Monitoring
{
    public interface IMonitoringLogReader
    {
        Task<IReadOnlyCollection<MonitoringLogEntry>> GetLogsAsync(int? organizationId, DateTime? from = null, DateTime? to = null);
    }
}
