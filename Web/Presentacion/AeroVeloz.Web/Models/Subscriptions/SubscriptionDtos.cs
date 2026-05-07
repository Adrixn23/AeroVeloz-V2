using System.Text.Json.Serialization;

namespace AeroVeloz.Web.Models.Subscriptions
{
    public class SubscriptionCountDto
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
