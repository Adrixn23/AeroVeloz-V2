namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightBatchItemDto
    {
        public string? CodeAirlines { get; init; }
        public string? OriginAirport { get; init; }
        public string? DestinationAirport { get; init; }
        public DateTimeOffset ScheduledDeparture { get; init; }
        public string? BoardingGate { get; init; }
        public string? BoardingGateArrived { get; init; }
    }
}
