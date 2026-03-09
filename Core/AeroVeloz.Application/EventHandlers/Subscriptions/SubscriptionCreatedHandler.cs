using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Domain.Common.Notification;
using AeroVeloz.Domain.Entities.Notification;
using AeroVeloz.Domain.Events.EventsSubscriptions;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.EventHandlers.Subscriptions
{
    public class SubscriptionCreatedHandler : INotificationHandler<SubscriptionCreated>
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly INotificationDispatcher _dispatcher;
        private readonly IOrganizationMonitoringLogger _logger;

        public SubscriptionCreatedHandler(
            INotificationRepository notificationRepo,
            INotificationDispatcher dispatcher,
            IOrganizationMonitoringLogger logger)
        {
            _notificationRepo = notificationRepo;
            _dispatcher = dispatcher;
            _logger = logger;
        }

        public async Task Handle(SubscriptionCreated notification, CancellationToken cancellationToken)
        {
            try
            {
                var channelType = notification.Channel switch
                {
                    Domain.Common.Enums.SubscriptionChannel.Email => ChannelType.Email,
                    Domain.Common.Enums.SubscriptionChannel.Sms => ChannelType.Sms,
                    Domain.Common.Enums.SubscriptionChannel.Push => ChannelType.Push,
                    _ => ChannelType.Push
                };

                var message = $"Se ha registrado su suscripción al vuelo {notification.AirlineCode}-{notification.FlightId}. Recibirá actualizaciones de estado.";

                var entity = new Notification
                {
                    Id = Guid.NewGuid(),
                    subscriptionId = notification.SubscriptionId,
                    codeProvider = (byte)channelType,
                    message = message,
                    createAt = DateTime.UtcNow,
                    statusNotification = "Sent"
                };
                await _notificationRepo.CreateAsync(entity);

                await _dispatcher.DispatchAsync(new NotificationPayload
                {
                    Title = "Suscripción confirmada",
                    Message = message,
                    Channel = channelType,
                    TargetExternalIds = [notification.ContactValue]
                });
            }
            catch (Exception ex)
            {
                await _logger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(SubscriptionCreatedHandler),
                    Message = $"Error al confirmar suscripción {notification.SubscriptionId}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
            }
        }
    }
}
