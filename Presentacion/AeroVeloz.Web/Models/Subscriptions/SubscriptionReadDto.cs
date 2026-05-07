using System.Text.Json.Serialization;

namespace AeroVeloz.Web.Models.Subscriptions
{
    public class SubscriptionReadDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("contactValue")]
        public string? ContactValue { get; set; }

        [JsonPropertyName("createDate")]
        public DateTime CreateDate { get; set; }

        [JsonPropertyName("activeSubscription")]
        public bool ActiveSubscription { get; set; }
    }
}
