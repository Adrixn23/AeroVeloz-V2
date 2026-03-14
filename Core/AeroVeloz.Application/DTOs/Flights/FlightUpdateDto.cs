namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightUpdateDto(
        short Id,
        string? CodeAirlinesIcao,
        string? OriginAirport,
        string? DestinationAirport,
        DateTimeOffset ScheduledDeparture,
        string? BordingGate,
        string? BoardingGateArrived,
        string? ChangeReason
    ) : FlightBaseDto(Id, CodeAirlinesIcao, OriginAirport, DestinationAirport, ScheduledDeparture);
}