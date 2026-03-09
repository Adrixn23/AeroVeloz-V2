using AeroVeloz.Domain.Events.Audit;
using AeroVeloz.Transversal.Contracts.Notifications;
using MediatR;

namespace AeroVeloz.Application.EventHandlers
{
    public class AuditEventHandler :
        INotificationHandler<AuditRecordCreatedDomainEvent>,
        INotificationHandler<AuditDeletionAttemptedDomainEvent>,
        INotificationHandler<AuditModificationAttemptedDomainEvent>,
        INotificationHandler<AuditIntegrityViolationDomainEvent>
    {
        private readonly INotificationDispatcher _dispatcher;

        public AuditEventHandler(INotificationDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task Handle(AuditRecordCreatedDomainEvent notification, CancellationToken ct)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(AuditDeletionAttemptedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = " Intento de eliminación de auditoría",
                Message = $"El usuario {notification.NameUser} intentó eliminar el registro de auditoría de {notification.EntityName}",
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.Push
            });
        }

        public async Task Handle(AuditModificationAttemptedDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = " Intento de modificación de auditoría",
                Message = $"El usuario {notification.NameUser} intentó modificar el registro de auditoría de {notification.EntityName}",
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.Push
            });
        }

        public async Task Handle(AuditIntegrityViolationDomainEvent notification, CancellationToken ct)
        {
            await _dispatcher.DispatchAsync(new NotificationPayload
            {
                Title = " Violación de integridad detectada",
                Message = $"Se detectó una violación de integridad en {notification.EntityName} de {notification.NameOrganization}",
                Detail = notification.ViolationDetail,
                OrganizationId = notification.IdOrganization,
                Channel = ChannelType.Push
            });
        }
    }
}
