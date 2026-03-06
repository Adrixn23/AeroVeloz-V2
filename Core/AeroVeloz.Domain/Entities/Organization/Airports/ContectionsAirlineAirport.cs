using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Organization.Airport
{
    public class ContectionsAirlineAirport : BEntity<Guid>
    {
        public string? codeAirlines { get; init; }
        public string? codeAirport { get; init;  }
        public string? tokenApi { get; init; }
        public bool isActive { get; init; }
        public DateTime createAt { get; init; }
    }
}
