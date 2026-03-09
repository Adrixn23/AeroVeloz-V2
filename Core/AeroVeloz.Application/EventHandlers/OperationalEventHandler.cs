using AeroVeloz.Domain.Events.Operations;
using AeroVeloz.Transversal.Contracts.Notifications;
using MediatR;

namespace AeroVeloz.Application.EventHandlers
{
    public class OperationalEventHandler :
        INotificationHandler<OperationalChangeRegisteredDomainEvent>,
        INotificationHandler<ChangeDoorEvent>
    {
        private readonly INotificationDispatcher _dispatcher;

        public OperationalEventHandler(INotificationDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Handle(OperationalChangeRegisteredDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Cambio operacional registrado",
                Message = $"Vuelo {notification.FlightNumber} - {notification.OperationalTypeName}: {notification.Cause}",
                Detail = $"Valor anterior: {notification.PreviousValue} → Nuevo: {notification.NewValue}",
                Channel = ChannelType.Push
            });
        }

        public async Task Handle(ChangeDoorEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Cambio de puerta de embarque",
                Message = $"Vuelo {notification.FlightNumber} ({notification.CodeAirline}) - Puerta: {notification.BoardingGateDeparture}",
                Detail = notification.Cause,
                Channel = ChannelType.Push
            });
        }
    }
}
