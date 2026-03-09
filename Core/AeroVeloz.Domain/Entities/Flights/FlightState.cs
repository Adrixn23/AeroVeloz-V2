using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Flights;

public partial class FlightState : BEntity<short>
{
    public string? code { get; init; }
    public string? StateName { get; init; }
}
