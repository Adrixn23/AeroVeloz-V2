namespace AeroVeloz.Transversal.Contracts.Notifications
{
    public interface INotificationChannel
    {
        ChannelType Channel { get; }
        Task SendAsync(NotificationPayload payload);
    }
}
