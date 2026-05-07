using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Application.Repositories.Subscriptions;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Notification;
using AeroVeloz.Domain.Entities.Notification;
using AeroVeloz.Domain.Events.EventsFlights;
using AeroVeloz.Domain.Events.EventsNotification;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.EventHandlers.Flights
{
    public class FlightDelayedHandler : INotificationHandler<FlightDelayed>
    {
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly INotificationRepository _notificationRepo;
        private readonly INotificationDispatcher _dispatcher;
        private readonly IMediator _mediator;
        private readonly IOrganizationMonitoringLogger _logger;

        public FlightDelayedHandler(
            ISubscriptionRepository subscriptionRepo,
            INotificationRepository notificationRepo,
            INotificationDispatcher dispatcher,
            IMediator mediator,
            IOrganizationMonitoringLogger logger)
        {
            _subscriptionRepo = subscriptionRepo;
            _notificationRepo = notificationRepo;
            _dispatcher = dispatcher;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(FlightDelayed notification, CancellationToken cancellationToken)
        {
            try
            {
                var subscribers = await _subscriptionRepo.GetActiveByFlightAsync(notification.FlightNumber, notification.AirlineCode);
                if (subscribers.Count == 0) return;

                var message = $"Vuelo {notification.AirlineCode}-{notification.FlightNumber}: retrasado. " +
                              $"Horario original: {notification.OriginalDeparture:HH:mm}. Se informará el nuevo horario.";

                foreach (var sub in subscribers)
                {
                    var channelType = sub.codeChannel switch
                    {
                        1 => ChannelType.Sms,
                        2 => ChannelType.Email,
                        3 => ChannelType.Push,
                        _ => ChannelType.Push
                    };

                    var notifId = Guid.NewGuid();
                    var entity = new Notification
                    {
                        Id = notifId,
                        subscripcionId = sub.Id, // Fixed property name
                        codeProvider = sub.codeChannel,
                        message = message,
                        createAt = DateTime.UtcNow,
                        statusNotification = NotificationDeliveryStatus.Pending.ToString()
                    };
                    await _notificationRepo.CreateAsync(entity);

                    try
                    {
                        await _dispatcher.DispatchAsync(new NotificationPayload
                        {
                            Title = $"Vuelo retrasado: {notification.AirlineCode}-{notification.FlightNumber}",
                            Message = message,
                            Channel = channelType,
                            TargetExternalIds = [sub.contactValue!]
                        });

                        await _notificationRepo.UpdateStatusAsync(notifId, NotificationDeliveryStatus.Sent.ToString());

                        await _mediator.Publish(new EventSendNotification(
                            notifId, sub.Id, notification.FlightNumber,
                            "Delayed", channelType.ToString(), message, DateTimeOffset.UtcNow), cancellationToken);
                    }
                    catch (Exception sendEx)
                    {
                        await _notificationRepo.UpdateStatusAsync(notifId, NotificationDeliveryStatus.Failed.ToString());

                        await _mediator.Publish(new EventFailedNotification(
                            notifId, sub.Id, notification.FlightNumber,
                            channelType.ToString(), message, sendEx.Message, 1, DateTimeOffset.UtcNow), cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(FlightDelayedHandler),
                    Message = $"Error procesando delay del vuelo {notification.FlightNumber}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
            }
        }
    }
}
