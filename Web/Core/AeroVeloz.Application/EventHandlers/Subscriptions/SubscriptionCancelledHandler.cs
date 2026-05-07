using AeroVeloz.Domain.Events.EventsSubscriptions;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.EventHandlers.Subscriptions
{
    public class SubscriptionCancelledHandler : INotificationHandler<SubscriptionCancelled>
    {
        private readonly IOrganizationMonitoringLogger _logger;

        public SubscriptionCancelledHandler(IOrganizationMonitoringLogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(SubscriptionCancelled notification, CancellationToken cancellationToken)
        {
            await _logger.LogAsync(new MonitoringLogEntry
            {
                Source = nameof(SubscriptionCancelledHandler),
                Message = $"Suscripción {notification.SubscriptionId} cancelada: {notification.Reason}",
                OccurredAt = notification.CancelledAt
            });
        }
    }
}
