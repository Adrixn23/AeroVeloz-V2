using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.BaseEntity;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Entities.Subscriptions;
namespace AeroVeloz.Domain.Entities.Flights;

public partial class Flight: BEntity<short>
{

    public string? codeAirlines { get; init; }
    public byte flightStateId { get; init; }

   
    public string? OriginAirport { get; init; }
    public string? DestinationAirport { get; init; }
    public DateTimeOffset ScheduledDeparture { get; init; }

    public string? BordingGate { get; init; }
    public string? BoardingGateArrived { get; init; }
}
