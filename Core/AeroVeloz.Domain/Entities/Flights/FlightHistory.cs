namespace AeroVeloz.Domain.Entities.Flights;

public partial class FlightHistory
{
    public short flightNumber { get; init; }
    public short codeAirlines { get; init; }
    public DateTime changeAt { get; init; }
    public string? reason { get; init; }
    public byte flightStatesIdAfter { get; init; }
    public byte flightStatesIdBefore { get; init; }
}