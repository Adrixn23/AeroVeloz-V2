using System.Net.Http.Json;
using AeroVeloz.Transversal.Contracts.Notifications;
using Microsoft.Extensions.Options;

namespace AeroVeloz.Infraestructure.Integrations.OneSignal
{
    public class OneSignalInAppChannel : INotificationChannel
    {
        private readonly HttpClient _httpClient;
        private readonly OneSignalOptions _options;

        public ChannelType Channel => ChannelType.InApp;

        public OneSignalInAppChannel(HttpClient httpClient, IOptions<OneSignalOptions> options)
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
                channel_for_external_user_ids = "push",
                isAnyWeb = false,
                data = new { detail = payload.Detail, organizationId = payload.OrganizationId, type = "in_app" }
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
