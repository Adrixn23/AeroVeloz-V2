using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Toolkit.Uwp.Notifications;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations.Notifications
{
    public class NotificationService
    {
        private HubConnection _hubConnection;
        private readonly ISessionService _sessionService;
        private readonly string _hubUrl; 

        public Action<string, string> OnNotificationReceived;

        public NotificationService(ISessionService sessionService, IConfiguration configuration)
        {
            _sessionService = sessionService;
            var baseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7126";
            _hubUrl = baseUrl.TrimEnd('/') + "/hubs/notifications";
        }

        public async Task StartAsync()
        {
            if (_hubConnection != null)
                return;

            var organizationId = _sessionService.OrgId != 0 ? _sessionService.OrgId.ToString() : "";

            var url = string.IsNullOrEmpty(organizationId) 
                ? _hubUrl 
                : $"{_hubUrl}?organizationId={organizationId}";

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<NotificationMessage>("ReceiveNotification", (message) =>
            {
                if (message.Type == "Push" || message.Type == "InApp")
                {
                    try 
                    {
                        ToastNotificationManagerCompat.History.Clear();
                        new ToastContentBuilder()
                            .AddArgument("action", "viewConversation")
                            .AddArgument("conversationId", 9813)
                            .AddText(message.Title)
                            .AddText(message.Message)
                            .Show(); 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error showing Toast Notification: {ex.Message}");
                    }
                }

                
                OnNotificationReceived?.Invoke(message.Title, message.Message);
            });

            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to SignalR: {ex.Message}");
            }
        }
        
        public async Task StopAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }
    }

    public class NotificationMessage
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public int Channel { get; set; }
    }
}
