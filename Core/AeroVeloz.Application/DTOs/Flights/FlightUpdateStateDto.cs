namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightUpdateStateDto
    {
        public short FlightNumber { get; init; }
        public string? CodeAirlines { get; init; }
        public byte NewFlightStateId { get; init; }
        public string? Reason { get; init; }
    }
}
