namespace AeroVeloz.Application.DTOs.Flights
{
    public abstract record FlightBaseDto(
        short Id,
        string? CodeAirlinesIcao,
        string? OriginAirport,
        string? DestinationAirport,
        DateTimeOffset ScheduledDeparture
    );
}