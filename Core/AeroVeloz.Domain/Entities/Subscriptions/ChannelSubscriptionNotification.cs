using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Subscriptions
{
    public partial class ChannelSubscriptionNotification: BEntity<byte>
    {
        public string? name { get; init; }
    }
}
