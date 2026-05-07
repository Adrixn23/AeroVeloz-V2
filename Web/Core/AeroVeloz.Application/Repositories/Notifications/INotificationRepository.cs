using AeroVeloz.Domain.Entities.Notification;

namespace AeroVeloz.Application.Repositories.Notifications
{
    public interface INotificationRepository
    {
        Task<bool> CreateAsync(Notification notification);
        Task<bool> UpdateStatusAsync(Guid notificationId, string newStatus);
        Task<IReadOnlyCollection<Notification>> GetBySubscriptionAsync(Guid subscriptionId);
    }
}
