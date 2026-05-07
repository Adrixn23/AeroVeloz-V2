namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightDetailsDto
    {
        public short Id { get; init; }
        public string? CodeAirlineIcao { get; init; }
        public string? FlightNumber { get; init; }
        public string? OriginAirport { get; init; }
        public string? DestinationAirport { get; init; }
        public DateTimeOffset ScheduledDeparture { get; init; }
        public string? BordingGate { get; init; }
        public string? BoardingGateArrived { get; init; }
        public byte FlightStateId { get; init; }
        public string? FlightStateName { get; init; }
        public int TotalOperations { get; init; }
        public int ActiveOperations { get; init; }
    }
}
