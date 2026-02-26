using System.ComponentModel.DataAnnotations;

namespace AeroVeloz.Domain.DomainServices.Interfaces.Notification
{
    public interface INotificationDomainService
    {
        Task<ValidationResult> GetAllNotificationAwaitAsync();
        Task<ValidationResult> GetAllSubscriptionNotificationsUsersInterested();
        Task<ValidationResult> addNotification(AeroVeloz.Domain.Entities.Notification.Notification notification);
        Task<ValidationResult> TriggeNotifications(/*IEnumerable<Subscriptions> lista de subscriptos para lanzar notificaciones*/);
    }
}
