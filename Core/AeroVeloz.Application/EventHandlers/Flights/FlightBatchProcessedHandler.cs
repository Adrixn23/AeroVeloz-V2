using AeroVeloz.Domain.Events.EventsAirlines;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.EventHandlers.Flights
{
    public class FlightBatchProcessedHandler : INotificationHandler<FlightBatchProcessed>
    {
        private readonly IOrganizationMonitoringLogger _logger;

        public FlightBatchProcessedHandler(IOrganizationMonitoringLogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(FlightBatchProcessed notification, CancellationToken cancellationToken)
        {
            await _logger.LogAsync(new MonitoringLogEntry
            {
                Source = nameof(FlightBatchProcessedHandler),
                Message = $"Lote de {notification.TotalFlightProcessedAT} vuelos procesado para aerolínea {notification.AirlineCode}",
                OccurredAt = notification.ProcessedAT
            });
        }
    }
}
