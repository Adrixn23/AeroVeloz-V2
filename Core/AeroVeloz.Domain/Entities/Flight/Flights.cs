using AeroVeloz.Domain.Entities.BaseEntity;
namespace AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Common.Enums;



public class Flights : BEntity<short>
{

    public string? codeAirlines { get; init; }
    public FlightStateEnum FlightStated { get;  init; }
    public string? OriginAirport { get; init; } 
    public string? DestinationAirport { get; init; }
    public DateTimeOffset ScheduledDeparture { get; init; }

    public DateTimeOffset ScheduledArrival { get; init; }

    public string? BoardingGate { get; init; }
    public string? BoardingGateArrived { get; init; }


}
