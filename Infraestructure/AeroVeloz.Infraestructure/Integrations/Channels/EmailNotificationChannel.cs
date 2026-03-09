using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Domain.Common.Notification;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Integrations.Channels
{
    public class EmailNotificationChannel : INotificationChannel
    {
        private readonly ILogger<EmailNotificationChannel> _logger;

        public EmailNotificationChannel(ILogger<EmailNotificationChannel> logger)
        {
            _logger = logger;
        }

        public ChannelType Channel => ChannelType.Email;

        public Task SendAsync(NotificationPayload payload)
        {
            _logger.LogInformation("[EMAIL] To: {Targets} | {Title}: {Message}",
                string.Join(",", payload.TargetExternalIds), payload.Title, payload.Message);
            return Task.CompletedTask;
        }
    }
}
