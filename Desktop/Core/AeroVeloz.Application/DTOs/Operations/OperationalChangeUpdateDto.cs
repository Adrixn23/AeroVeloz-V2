namespace AeroVeloz.Application.DTOs.Operations
{
    public sealed record OperationalChangeUpdateDto(
        Guid Id,
        short IdOperationalType,
        short FlightNumber,
        string? CodeAirline,
        string? CodeAirport,
        string? PreviousValue,
        string? NewValue,
        string? Cause
    );
}
