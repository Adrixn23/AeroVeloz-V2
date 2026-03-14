namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightQueryDto(
        string? CodeAirlinesIcao = null,
        string? OriginAirport = null,
        string? DestinationAirport = null,
        DateTimeOffset? DepartureDateFrom = null,
        DateTimeOffset? DepartureDateTo = null,
        byte? FlightStateId = null
    );
}