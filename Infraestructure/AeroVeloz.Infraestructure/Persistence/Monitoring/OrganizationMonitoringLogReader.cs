using System.Text.Json;
using AeroVeloz.Transversal.Monitoring;
using AeroVeloz.Transversal.Contracts.Monitoring;

namespace AeroVeloz.Infraestructure.Persistence.Monitoring
{
    public class OrganizationMonitoringLogReader : IMonitoringLogReader
    {
        private readonly string _basePath;

        public OrganizationMonitoringLogReader()
        {
            _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        }

        public async Task<IReadOnlyCollection<MonitoringLogEntry>> GetLogsAsync(
            int? organizationId, DateTime? from = null, DateTime? to = null)
        {
            var entries = new List<MonitoringLogEntry>();

            if (organizationId.HasValue)
            {
                var folder = Path.Combine(_basePath, $"org_{organizationId}");
                await ReadFromFolderAsync(folder, from, to, entries);
            }
            else
            {
                if (!Directory.Exists(_basePath))
                    return Array.Empty<MonitoringLogEntry>();

                foreach (var folder in Directory.GetDirectories(_basePath))
                {
                    await ReadFromFolderAsync(folder, from, to, entries);
                }
            }

            return entries
                .OrderByDescending(e => e.OccurredAt)
                .ToList()
                .AsReadOnly();
        }

        private static async Task ReadFromFolderAsync(
            string folder, DateTime? from, DateTime? to, List<MonitoringLogEntry> entries)
        {
            if (!Directory.Exists(folder))
                return;

            var logFiles = Directory.GetFiles(folder, "*.log");

            foreach (var filePath in logFiles)
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                if (!DateOnly.TryParseExact(fileName, "yyyy-MM-dd", out var fileDate))
                    continue;

                if (from.HasValue && fileDate < DateOnly.FromDateTime(from.Value))
                    continue;
                if (to.HasValue && fileDate > DateOnly.FromDateTime(to.Value))
                    continue;

                var lines = await File.ReadAllLinesAsync(filePath);

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var entry = JsonSerializer.Deserialize<MonitoringLogEntry>(line);
                    if (entry == null)
                        continue;

                    if (from.HasValue && entry.OccurredAt < from.Value)
                        continue;
                    if (to.HasValue && entry.OccurredAt > to.Value)
                        continue;

                    entries.Add(entry);
                }
            }
        }
    }
}
