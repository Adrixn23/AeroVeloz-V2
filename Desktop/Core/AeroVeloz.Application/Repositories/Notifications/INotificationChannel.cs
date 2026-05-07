using AeroVeloz.Domain.Common.Notification;

namespace AeroVeloz.Application.Repositories.Notifications
{
    public interface INotificationChannel
    {
        ChannelType Channel { get; }
        Task SendAsync(NotificationPayload payload);
    }
}
