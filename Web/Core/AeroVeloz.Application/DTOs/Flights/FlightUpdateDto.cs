namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightUpdateDto(
        short Id,
        string? codeAirlinesIcao,
        string? OriginAirport,
        string? DestinationAirport,
        DateTimeOffset ScheduledDeparture,
        string? BoardingGate,
        string? BoardingGateArrived,
        string? ChangeReason
    ) : FlightBaseDto(Id, codeAirlinesIcao, OriginAirport, DestinationAirport, ScheduledDeparture);
}