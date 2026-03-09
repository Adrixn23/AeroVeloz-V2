namespace AeroVeloz.Transversal.Contracts.Notifications
{
    public interface INotificationDispatcher
    {
        Task DispatchAsync(NotificationPayload payload);
        Task DispatchAsync(IEnumerable<NotificationPayload> payloads);
    }
}
