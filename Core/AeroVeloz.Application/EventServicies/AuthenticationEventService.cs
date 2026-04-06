using AeroVeloz.Domain.Events.User;
using AeroVeloz.Domain.Common.Notification;
using AeroVeloz.Application.Repositories.Notifications;
using MediatR;

namespace AeroVeloz.Application.EventHandlers
{
    public class AuthenticationEventService :
        INotificationHandler<UserLoginFailedDomainEvent>,
        INotificationHandler<UserAccountLockedDomainEvent>
    {
        private readonly INotificationDispatcher _dispatcher;

        public AuthenticationEventService(INotificationDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Handle(UserLoginFailedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Intento de inicio de sesión fallido",
                Message = $"El usuario {notification.NameUser} ha fallado el intento de inicio de sesión #{notification.FailedAttempts} en {notification.NameOrganization}",
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.InApp
            });
        }

        public async Task Handle(UserAccountLockedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Cuenta de usuario bloqueada",
                Message = $"La cuenta del usuario {notification.NameUser} ha sido bloqueada hasta {notification.LockedUntil:yyyy-MM-dd HH:mm} por {notification.FailedAttempts} intentos fallidos",
                Detail = $"Organización: {notification.NameOrganization}",
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.Push
            });
        }
    }
}
