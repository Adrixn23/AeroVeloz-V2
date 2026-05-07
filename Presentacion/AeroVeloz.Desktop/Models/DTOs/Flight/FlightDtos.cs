namespace AeroVeloz.Desktop.Models.DTOs.Flight
{
    public sealed record FlightForOperationDto
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

    public sealed record FlightOperationDto
    {
        public Guid Id { get; init; }
        public string? OperationalTypeName { get; init; }
        public string? PreviousValue { get; init; }
        public string? NewValue { get; init; }
        public DateTime ChangeAt { get; init; }
        public string? Cause { get; init; }
        public bool IsActive { get; init; }
    }
}
