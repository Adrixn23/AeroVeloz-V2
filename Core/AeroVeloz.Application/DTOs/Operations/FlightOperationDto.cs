namespace AeroVeloz.Application.DTOs.Operations
{
    public sealed record FlightOperationDto
    {
        public Guid Id { get; init; }
        public short IdOperationalType { get; init; }
        public string? OperationalTypeName { get; init; }
        public short FlightNumber { get; init; }
        public string? CodeAirline { get; init; }
        public string? CodeAirport { get; init; }
        public string? PreviousValue { get; init; }
        public string? NewValue { get; init; }
        public DateTime ChangeAt { get; init; }
        public string? Cause { get; init; }
        public bool IsActive { get; init; }
        public Guid UserId { get; init; }
    }
}
