using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AeroVeloz.Infraestructure.Integrations.Notifications.SignalR
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var organizationId = Context.GetHttpContext()?.Request.Query["organizationId"].ToString();
            
            if (!string.IsNullOrEmpty(organizationId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Org_{organizationId}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var organizationId = Context.GetHttpContext()?.Request.Query["organizationId"].ToString();
            
            if (!string.IsNullOrEmpty(organizationId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Org_{organizationId}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}