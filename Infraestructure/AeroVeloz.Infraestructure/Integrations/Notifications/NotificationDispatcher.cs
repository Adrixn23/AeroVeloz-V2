using AeroVeloz.Transversal.Contracts.Notifications;

namespace AeroVeloz.Infraestructure.Integrations.Notifications
{
    public class NotificationDispatcher : INotificationDispatcher
    {
        private readonly IReadOnlyDictionary<ChannelType, INotificationChannel> _channels;

        public NotificationDispatcher(IEnumerable<INotificationChannel> channels)
        {
            _channels = channels.ToDictionary(c => c.Channel);
        }

        public async Task DispatchAsync(NotificationPayload payload)
        {
            if (_channels.TryGetValue(payload.Channel, out var channel))
                await channel.SendAsync(payload);
        }

        public async Task DispatchAsync(IEnumerable<NotificationPayload> payloads)
        {
            foreach (var payload in payloads)
                await DispatchAsync(payload);
        }
    }
}
