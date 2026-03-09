namespace AeroVeloz.Application.DTOs.Operations
{
    public sealed record OperationalChangeSaveDto
    {
        public short IdOperationalType { get; init; }
        public short FlightNumber { get; init; }
        public string? CodeAirline { get; init; }
        public string? CodeAirport { get; init; }
        public string? PreviousValue { get; init; }
        public string? NewValue { get; init; }
        public string? Cause { get; init; }
    }
}
