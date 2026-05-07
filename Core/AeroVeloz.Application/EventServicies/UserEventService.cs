using AeroVeloz.Domain.Events.User;
using AeroVeloz.Domain.Common.Notification;
using AeroVeloz.Application.Repositories.Notifications;
using MediatR;

namespace AeroVeloz.Application.EventHandlers
{
    public class UserEventService :
        INotificationHandler<UserCreatedDomainEvent>,
        INotificationHandler<UserUpdatedDomainEvent>,
        INotificationHandler<UserDeactivatedDomainEvent>,
        INotificationHandler<UserAccountLockedDomainEvent>,
        INotificationHandler<UserLoginFailedDomainEvent>
    {
        private readonly INotificationDispatcher _dispatcher;

        public UserEventService(INotificationDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Nuevo usuario registrado",
                Message = $"Se ha creado el usuario {notification.NameUser} con rol {notification.NameRol} en la organización {notification.NameOrganization}",
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.Push
            });
        }

        public async Task Handle(UserUpdatedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Usuario actualizado",
                Message = $"El usuario {notification.NameUser} ha sido modificado",
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.InApp
            });
        }

        public async Task Handle(UserDeactivatedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Usuario desactivado",
                Message = $"El usuario {notification.NameUser} ha sido desactivado de {notification.NameOrganization}",
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.Push
            });
        }

        public async Task Handle(UserAccountLockedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Cuenta de usuario bloqueada por seguridad",
                Message = $"La cuenta del usuario {notification.NameUser} en {notification.NameOrganization} ha sido bloqueada hasta {notification.LockedUntil:yyyy-MM-dd HH:mm} UTC por {notification.FailedAttempts} intentos fallidos de inicio de sesión",
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.Push
            });
        }

        public async Task Handle(UserLoginFailedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Intento de inicio de sesión fallido",
                Message = $"El usuario {notification.NameUser} ha realizado el intento fallido #{notification.FailedAttempts} en {notification.NameOrganization}",
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.InApp
            });
        }
    }
}
