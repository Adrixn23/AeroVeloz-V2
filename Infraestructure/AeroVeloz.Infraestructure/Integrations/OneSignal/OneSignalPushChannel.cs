using System.Net.Http.Json;
using AeroVeloz.Transversal.Contracts.Notifications;
using Microsoft.Extensions.Options;

namespace AeroVeloz.Infraestructure.Integrations.OneSignal
{
    public class OneSignalPushChannel : INotificationChannel
    {
        private readonly HttpClient _httpClient;
        private readonly OneSignalOptions _options;

        public ChannelType Channel => ChannelType.Push;

        public OneSignalPushChannel(HttpClient httpClient, IOptions<OneSignalOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task SendAsync(NotificationPayload payload)
        {
            var body = new
            {
                app_id = _options.AppId,
                headings = new { en = payload.Title },
                contents = new { en = payload.Message },
                include_external_user_ids = payload.TargetExternalIds,
                data = new { detail = payload.Detail, organizationId = payload.OrganizationId }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.onesignal.com/notifications")
            {
                Content = JsonContent.Create(body)
            };
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {_options.RestApiKey}");

            await _httpClient.SendAsync(request);
        }
    }
}
