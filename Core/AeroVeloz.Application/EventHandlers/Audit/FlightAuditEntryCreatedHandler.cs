using AeroVeloz.Domain.Events.EventsAirlines;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.EventHandlers.Audit
{
    public class FlightAuditEntryCreatedHandler : INotificationHandler<FlightAuditEntryCreated>
    {
        private readonly IOrganizationMonitoringLogger _logger;

        public FlightAuditEntryCreatedHandler(IOrganizationMonitoringLogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(FlightAuditEntryCreated notification, CancellationToken cancellationToken)
        {
            await _logger.LogAsync(new MonitoringLogEntry
            {
                Source = nameof(FlightAuditEntryCreatedHandler),
                Message = $"[AUDIT] {notification.ActorType}: {notification.ActionDetail}",
                Detail = notification.NewValuesJson,
                OccurredAt = notification.Timestamp
            });
        }
    }
}
