namespace AeroVeloz.Application.DTOs.Flights
{
    public abstract record FlightBaseDto(
        short Id,
        string? codeAirlinesIcao,
        string? OriginAirport,
        string? DestinationAirport,
        DateTimeOffset ScheduledDeparture
    );
}