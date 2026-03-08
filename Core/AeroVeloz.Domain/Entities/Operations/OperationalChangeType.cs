using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Operations
{
    public partial class OperationalChangeType:BEntity<short>
    {
        public string? name { get; init; }
    }
}
