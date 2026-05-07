using AeroVeloz.Domain.Events.EventsNotification;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.EventHandlers.Notifications
{
    public class NotificationFailedHandler : INotificationHandler<EventFailedNotification>
    {
        private readonly IOrganizationMonitoringLogger _logger;

        public NotificationFailedHandler(IOrganizationMonitoringLogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(EventFailedNotification notification, CancellationToken cancellationToken)
        {
            await _logger.LogSystemFaultAsync(new MonitoringLogEntry
            {
                Source = nameof(NotificationFailedHandler),
                Message = $"Notificación {notification.NotificationId} FALLIDA vía {notification.TransportChannel} " +
                          $"para vuelo {notification.FlightNumber} | Intento: {notification.RetryCount} | Razón: {notification.ErrorReason}",
                OccurredAt = notification.FailedAt.UtcDateTime
            });
        }
    }
}
