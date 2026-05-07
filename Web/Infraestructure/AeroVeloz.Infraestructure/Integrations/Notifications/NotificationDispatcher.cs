using AeroVeloz.Domain.Common.Notification;
using AeroVeloz.Application.Repositories.Notifications;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Integrations.Notifications
{
    public class NotificationDispatcher : INotificationDispatcher
    {
        private readonly IReadOnlyDictionary<ChannelType, INotificationChannel> _channels;
        private readonly ILogger<NotificationDispatcher> _logger;

        public NotificationDispatcher(IEnumerable<INotificationChannel> channels, ILogger<NotificationDispatcher> logger)
        {
            _channels = channels.ToDictionary(c => c.Channel);
            _logger = logger;
        }

        public async Task DispatchAsync(NotificationPayload payload)
        {
            if (!_channels.TryGetValue(payload.Channel, out var channel))
            {
                _logger.LogWarning("No se encontró canal registrado para {Channel}", payload.Channel);
                throw new InvalidOperationException($"Canal {payload.Channel} no registrado");
            }

            await channel.SendAsync(payload);
        }

        public async Task DispatchAsync(IEnumerable<NotificationPayload> payloads)
        {
            foreach (var payload in payloads)
                await DispatchAsync(payload);
        }
    }
}
