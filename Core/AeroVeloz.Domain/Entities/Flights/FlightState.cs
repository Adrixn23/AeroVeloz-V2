using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Flights;

public partial class FlightState : BEntity<byte>
{
    public string? codeFlightState { get; init; }
    public string? name { get; init; }
}
