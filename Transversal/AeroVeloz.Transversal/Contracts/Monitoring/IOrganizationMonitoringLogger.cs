using AeroVeloz.Transversal.Monitoring;

namespace AeroVeloz.Transversal.Contracts.Monitoring
{

    public interface IOrganizationMonitoringLogger
    {
      
        Task LogAsync(MonitoringLogEntry entry);

        Task LogSecurityAlertAsync(MonitoringLogEntry entry);

      
        Task LogSystemFaultAsync(MonitoringLogEntry entry, Exception? exception = null);
    }
}
