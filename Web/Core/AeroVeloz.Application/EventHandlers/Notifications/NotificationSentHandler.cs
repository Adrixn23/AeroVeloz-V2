using AeroVeloz.Domain.Events.EventsNotification;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.EventHandlers.Notifications
{
    public class NotificationSentHandler : INotificationHandler<EventSendNotification>
    {
        private readonly IOrganizationMonitoringLogger _logger;

        public NotificationSentHandler(IOrganizationMonitoringLogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(EventSendNotification notification, CancellationToken cancellationToken)
        {
            await _logger.LogAsync(new MonitoringLogEntry
            {
                Source = nameof(NotificationSentHandler),
                Message = $"Notificación {notification.NotificationId} enviada vía {notification.TransportChannel} " +
                          $"para vuelo {notification.FlightNumber} (estado: {notification.FlightStatus})",
                OccurredAt = notification.SentAt.UtcDateTime
            });
        }
    }
}
