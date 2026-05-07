using AeroVeloz.Domain.Events.Aiport;
using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Domain.Common.Notification;
using MediatR;
using System.Diagnostics;

namespace AeroVeloz.Application.EventHandlers
{
    public class AirportEventService :
        INotificationHandler<AirportRegisteredDomainEvent>,
        INotificationHandler<AirportUpdatedDomainEvent>,
        INotificationHandler<AirportSuspendedDomainEvent>,
        INotificationHandler<AirportConnectionCreatedDomainEvent>,
        INotificationHandler<AirportConnectionDeactivatedDomainEvent>,
        INotificationHandler<AirportApiKeyGeneratedDomainEvent>
    {
        private readonly INotificationDispatcher _dispatcher;

        public AirportEventService(INotificationDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Handle(AirportRegisteredDomainEvent notification, CancellationToken ct)
        {
            try
            {
                await _dispatcher.DispatchAsync(new NotificationPayload
                {
                    Title = "Nuevo aeropuerto registrado",
                    Message = $"Se ha registrado el aeropuerto {notification.NameAirport} ({notification.CodeAirportICAO})",
                    Channel = ChannelType.Push
                });

                if (!string.IsNullOrWhiteSpace(notification.EmailOrganization))
                {
                    await _dispatcher.DispatchAsync(new NotificationPayload
                    {
                        Title = "Bienvenido a AeroVeloz - Credenciales de acceso",
                        Message = $"Su aeropuerto {notification.NameAirport} ({notification.CodeAirportICAO}) ha sido registrado exitosamente. " +
                                  $"Usuario: {notification.DefaultUserName} | Contraseña: {notification.DefaultPassword}",
                        Detail = "Por favor cambie su contraseña después del primer inicio de sesión. La contraseña proporcionada está encriptada y solo es visible en su servicio de mensajería.",
                        EmailAddress = notification.EmailOrganization,
                        Channel = ChannelType.Email
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en AirportEventService.Handle(AirportRegisteredDomainEvent): {ex.Message}");
            }
        }

        public async Task Handle(AirportUpdatedDomainEvent notification, CancellationToken ct)
        {
            try
            {
                await _dispatcher.DispatchAsync(new NotificationPayload
                {
                    Title = "Aeropuerto actualizado",
                    Message = $"El aeropuerto {notification.NameAirport} ha sido modificado",
                    Channel = ChannelType.InApp
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en AirportEventService.Handle(AirportUpdatedDomainEvent): {ex.Message}");
            }
        }

        public async Task Handle(AirportSuspendedDomainEvent notification, CancellationToken ct)
        {
            try
            {
                await _dispatcher.DispatchAsync(new NotificationPayload
                {
                    Title = "Aeropuerto suspendido",
                    Message = $"El aeropuerto {notification.NameAirport} ha sido suspendido",
                    Channel = ChannelType.Push
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en AirportEventService.Handle(AirportSuspendedDomainEvent): {ex.Message}");
            }
        }

        public async Task Handle(AirportConnectionCreatedDomainEvent notification, CancellationToken ct)
        {
            try
            {
                await _dispatcher.DispatchAsync(new NotificationPayload
                {
                    Title = "Nueva conexión aeropuerto-aerolínea",
                    Message = $"Conexión creada entre {notification.CodeAirport} y {notification.CodeAirline}",
                    Channel = ChannelType.InApp
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en AirportEventService.Handle(AirportConnectionCreatedDomainEvent): {ex.Message}");
            }
        }

        public async Task Handle(AirportConnectionDeactivatedDomainEvent notification, CancellationToken ct)
        {
            try
            {
                await _dispatcher.DispatchAsync(new NotificationPayload
                {
                    Title = "Conexión desactivada",
                    Message = $"Se ha desactivado la conexión entre {notification.CodeAirport} y {notification.CodeAirline}",
                    Channel = ChannelType.Push
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en AirportEventService.Handle(AirportConnectionDeactivatedDomainEvent): {ex.Message}");
            }
        }

        public async Task Handle(AirportApiKeyGeneratedDomainEvent notification, CancellationToken ct)
        {
            try
            {
                await _dispatcher.DispatchAsync(new NotificationPayload
                {
                    Title = "API Key generada",
                    Message = $"Se ha generado una nueva API Key para {notification.NameAirport}",
                    Channel = ChannelType.Push
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en AirportEventService.Handle(AirportApiKeyGeneratedDomainEvent): {ex.Message}");
            }
        }
    }
}
