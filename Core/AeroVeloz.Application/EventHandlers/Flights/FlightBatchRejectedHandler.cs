using AeroVeloz.Domain.Events.EventsAirlines;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.EventHandlers.Flights
{
    public class FlightBatchRejectedHandler : INotificationHandler<FlightBatchRejected>
    {
        private readonly IOrganizationMonitoringLogger _logger;

        public FlightBatchRejectedHandler(IOrganizationMonitoringLogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(FlightBatchRejected notification, CancellationToken cancellationToken)
        {
            await _logger.LogAsync(new MonitoringLogEntry
            {
                Source = nameof(FlightBatchRejectedHandler),
                Message = $"Vuelos rechazados para aerolínea {notification.AirlineCode}: {notification.Reason}",
                OccurredAt = notification.RejectedAt
            });
        }
    }
}
