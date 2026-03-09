using AeroVeloz.Domain.Events.Aiport;
using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Domain.Common.Notification;
using MediatR;

namespace AeroVeloz.Application.EventHandlers
{
    public class AirportEventServicie :
        INotificationHandler<AirportRegisteredDomainEvent>,
        INotificationHandler<AirportUpdatedDomainEvent>,
        INotificationHandler<AirportSuspendedDomainEvent>,
        INotificationHandler<AirportConnectionCreatedDomainEvent>,
        INotificationHandler<AirportConnectionDeactivatedDomainEvent>,
        INotificationHandler<AirportApiKeyGeneratedDomainEvent>
    {
        private readonly INotificationDispatcher _dispatcher;

        public AirportEventServicie(INotificationDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Handle(AirportRegisteredDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Nuevo aeropuerto registrado",
                Message = $"Se ha registrado el aeropuerto {notification.NameAirport} ({notification.CodeAirportICAO})",
                Channel = ChannelType.Push
            });
        }

        public async Task Handle(AirportUpdatedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Aeropuerto actualizado",
                Message = $"El aeropuerto {notification.NameAirport} ha sido modificado",
                Channel = ChannelType.InApp
            });
        }

        public async Task Handle(AirportSuspendedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Aeropuerto suspendido",
                Message = $"El aeropuerto {notification.NameAirport} ha sido suspendido",
                Channel = ChannelType.Push
            });
        }

        public async Task Handle(AirportConnectionCreatedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Nueva conexión aeropuerto-aerolínea",
                Message = $"Conexión creada entre {notification.CodeAirport} y {notification.CodeAirline}",
                Channel = ChannelType.InApp
            });
        }

        public async Task Handle(AirportConnectionDeactivatedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Conexión desactivada",
                Message = $"Se ha desactivado la conexión entre {notification.CodeAirport} y {notification.CodeAirline}",
                Channel = ChannelType.Push
            });
        }

        public async Task Handle(AirportApiKeyGeneratedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "API Key generada",
                Message = $"Se ha generado una nueva API Key para {notification.NameAirport}",
                Channel = ChannelType.Push
            });
        }
    }
}
