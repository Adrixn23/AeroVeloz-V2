using System.Text.Json;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Persistence.Monitoring
{
   
    public class OrganizationMonitoringLogger : IOrganizationMonitoringLogger
    {
        private readonly ILogger<OrganizationMonitoringLogger> _logger;
        private readonly string _basePath;
        private static readonly SemaphoreSlim _writeLock = new(1, 1);

        public OrganizationMonitoringLogger(ILogger<OrganizationMonitoringLogger> logger)
        {
            _logger = logger;
            _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        }

        public async Task LogAsync(MonitoringLogEntry entry)
        {
            _logger.LogInformation("[{OccurredAt}] Org:{OrgId} User:{UserId} Source:{Source} - {Message}",
                entry.OccurredAt, entry.OrganizationId, entry.UserId, entry.Source, entry.Message);

            await PersistToFileAsync(entry);
        }

        public async Task LogSecurityAlertAsync(MonitoringLogEntry entry)
        {
            _logger.LogWarning("[SECURITY] [{OccurredAt}] Org:{OrgId} User:{UserId} Source:{Source} - {Message}",
                entry.OccurredAt, entry.OrganizationId, entry.UserId, entry.Source, entry.Message);

            await PersistToFileAsync(entry);
        }

        public async Task LogSystemFaultAsync(MonitoringLogEntry entry, Exception? exception = null)
        {
            _logger.LogError(exception, "[FAULT] [{OccurredAt}] Org:{OrgId} User:{UserId} Source:{Source} - {Message}",
                entry.OccurredAt, entry.OrganizationId, entry.UserId, entry.Source, entry.Message);

            var entryWithDetail = exception != null
                ? entry with { Detail = entry.Detail ?? exception.ToString() }
                : entry;

            await PersistToFileAsync(entryWithDetail);
        }

        private async Task PersistToFileAsync(MonitoringLogEntry entry)
        {
            var folder = entry.OrganizationId.HasValue
                ? Path.Combine(_basePath, $"org_{entry.OrganizationId}")
                : Path.Combine(_basePath, "system");

            Directory.CreateDirectory(folder);

            var fileName = $"{entry.OccurredAt:yyyy-MM-dd}.log";
            var filePath = Path.Combine(folder, fileName);

            var jsonLine = JsonSerializer.Serialize(entry) + Environment.NewLine;

            await _writeLock.WaitAsync();
            try
            {
                await File.AppendAllTextAsync(filePath, jsonLine);
            }
            finally
            {
                _writeLock.Release();
            }
        }
    }
}
