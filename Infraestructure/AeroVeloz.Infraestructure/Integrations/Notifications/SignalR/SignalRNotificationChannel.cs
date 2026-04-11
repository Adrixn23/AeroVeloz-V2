using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Domain.Common.Notification;
using Microsoft.AspNetCore.SignalR;

namespace AeroVeloz.Infraestructure.Integrations.Notifications.SignalR
{
    public class SignalRNotificationChannel : INotificationChannel
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public ChannelType Channel { get; }

        public SignalRNotificationChannel(IHubContext<NotificationHub> hubContext, ChannelType channelType)
        {
            _hubContext = hubContext;
            Channel = channelType;
        }

        public async Task SendAsync(NotificationPayload payload)
        {
            var target = payload.OrganizationId.HasValue 
                ? _hubContext.Clients.Group($"Org_{payload.OrganizationId.Value}") 
                : _hubContext.Clients.All;
            
            await target.SendAsync("ReceiveNotification", new 
            {
                Type = Channel.ToString(),
                payload.Title, 
                payload.Message, 
                payload.Detail,
                payload.Channel
            });
        }
    }
}