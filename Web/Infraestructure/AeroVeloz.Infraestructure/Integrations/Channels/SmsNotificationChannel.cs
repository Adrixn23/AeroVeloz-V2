using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Domain.Common.Notification;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Integrations.Channels
{
    public class SmsNotificationChannel : INotificationChannel
    {
        private readonly ILogger<SmsNotificationChannel> _logger;

        public SmsNotificationChannel(ILogger<SmsNotificationChannel> logger)
        {
            _logger = logger;
        }

        public ChannelType Channel => ChannelType.Sms;

        public Task SendAsync(NotificationPayload payload)
        {
            _logger.LogInformation("[SMS] To: {Targets} | {Message}",
                string.Join(",", payload.TargetExternalIds), payload.Message);
            return Task.CompletedTask;
        }
    }
}
