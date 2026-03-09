using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Notification
{
    public partial class ProviderResponse: BEntity<byte>
    {
        public string? name { get; init; }
    }
}
