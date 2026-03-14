using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Application.Repositories.Subscriptions;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Notification;
using AeroVeloz.Domain.Entities.Notification;
using AeroVeloz.Domain.Events.EventsAirlines;
using AeroVeloz.Domain.Events.EventsNotification;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;
using MediatR;

namespace AeroVeloz.Application.EventHandlers.Flights
{
    public class FlightStateChangedHandler : INotificationHandler<FlightStateChangedByAirline>
    {
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly INotificationRepository _notificationRepo;
        private readonly INotificationDispatcher _dispatcher;
        private readonly IMediator _mediator;
        private readonly IOrganizationMonitoringLogger _logger;

        public FlightStateChangedHandler(
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

        public async Task Handle(FlightStateChangedByAirline notification, CancellationToken cancellationToken)
        {
            try
            {
                if (!short.TryParse(notification.FlightNumber, out var flightNumber))
                    return;

                var subscribers = await _subscriptionRepo.GetActiveByFlightAsync(flightNumber, notification.AirlineCode);
                if (subscribers.Count == 0) return;

                bool requiresSupervision = notification.NewState == (short)FlightStateEnum.Diverted;
                string statusLabel = notification.NewState.ToString();

                foreach (var sub in subscribers)
                {
                    string messageBody = BuildPublicMessage((FlightStateEnum)notification.NewState, notification.AirlineCode, flightNumber);

                    if (requiresSupervision && !sub.contactValue!.StartsWith("airline:"))
                    {
                        messageBody = $"Vuelo {notification.AirlineCode}-{flightNumber}: se ha registrado un cambio operativo. Pendiente de confirmación.";
                    }

                    var channelType = sub.codeChannel switch
                    {
                        1 => ChannelType.Sms,
                        2 => ChannelType.Email,
                        3 => ChannelType.Push,
                        _ => ChannelType.Push
                    };

                    var notifId = Guid.NewGuid();
                    var notifEntity = new Notification
                    {
                        Id = notifId,
                        subscripcionId = sub.Id,
                        codeProvider = sub.codeChannel,
                        message = messageBody,
                        createAt = DateTime.UtcNow,
                        statusNotification = NotificationDeliveryStatus.Pending.ToString()
                    };
                    await _notificationRepo.CreateAsync(notifEntity);

                    try
                    {
                        await _dispatcher.DispatchAsync(new NotificationPayload
                        {
                            Title = $"Vuelo {notification.AirlineCode}-{flightNumber} - {statusLabel}",
                            Message = messageBody,
                            Channel = channelType,
                            TargetExternalIds = [sub.contactValue!]
                        });

                        await _notificationRepo.UpdateStatusAsync(notifId, NotificationDeliveryStatus.Sent.ToString());

                        await _mediator.Publish(new EventSendNotification(
                            notifId, sub.Id, flightNumber, statusLabel,
                            channelType.ToString(), messageBody, DateTimeOffset.UtcNow), cancellationToken);
                    }
                    catch (Exception sendEx)
                    {
                        await _notificationRepo.UpdateStatusAsync(notifId, NotificationDeliveryStatus.Failed.ToString());

                        await _mediator.Publish(new EventFailedNotification(
                            notifId, sub.Id, flightNumber, channelType.ToString(),
                            messageBody, sendEx.Message, 1, DateTimeOffset.UtcNow), cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    Source = nameof(FlightStateChangedHandler),
                    Message = $"Error procesando cambio de estado vuelo {notification.FlightNumber}",
                    Detail = ex.Message,
                    OccurredAt = DateTime.UtcNow
                }, ex);
            }
        }

        private static string BuildPublicMessage(FlightStateEnum state, string airline, short flight) => state switch
        {
            FlightStateEnum.Scheduled => $"Vuelo {airline}-{flight} programado exitosamente.",
            FlightStateEnum.Boarding => $"Vuelo {airline}-{flight}: embarque iniciado.",
            FlightStateEnum.Delayed => $"Vuelo {airline}-{flight}: retrasado. Se informará el nuevo horario.",
            FlightStateEnum.InFlight => $"Vuelo {airline}-{flight}: en vuelo.",
            FlightStateEnum.Landed => $"Vuelo {airline}-{flight}: ha aterrizado.",
            FlightStateEnum.Completed => $"Vuelo {airline}-{flight}: operación finalizada. Gracias por su seguimiento.",
            FlightStateEnum.Cancelled => $"Vuelo {airline}-{flight}: cancelado. Contacte a la aerolínea.",
            FlightStateEnum.Diverted => $"Vuelo {airline}-{flight}: desviado. Información pendiente de confirmación.",
            _ => $"Vuelo {airline}-{flight}: actualización de estado."
        };
    }
}
