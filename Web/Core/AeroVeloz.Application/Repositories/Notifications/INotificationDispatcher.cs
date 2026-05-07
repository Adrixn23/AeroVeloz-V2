using AeroVeloz.Domain.Common.Notification;

namespace AeroVeloz.Application.Repositories.Notifications
{
    public interface INotificationDispatcher
    {
        Task DispatchAsync(NotificationPayload payload);
        Task DispatchAsync(IEnumerable<NotificationPayload> payloads);
    }
}
