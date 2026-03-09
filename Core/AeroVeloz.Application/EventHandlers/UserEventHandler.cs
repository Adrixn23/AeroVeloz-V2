using AeroVeloz.Domain.Events.User;
using AeroVeloz.Transversal.Contracts.Notifications;
using MediatR;

namespace AeroVeloz.Application.EventHandlers
{
    public class UserEventHandler :
        INotificationHandler<UserCreatedDomainEvent>,
        INotificationHandler<UserUpdatedDomainEvent>,
        INotificationHandler<UserDeactivatedDomainEvent>
    {
        private readonly INotificationDispatcher _dispatcher;

        public UserEventHandler(INotificationDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = "Nuevo usuario registrado",
                Message = $"Se ha creado el usuario {notification.NameUser} en la organización",
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
    }
}
