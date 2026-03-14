namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightBatchItemDto(
        short Id, // Número de vuelo
        string? CodeAirlinesIcao,
        string? OriginAirport,
        string? DestinationAirport,
        DateTimeOffset ScheduledDeparture,
        string? BoardingGate,
        string? BoardingGateArrived
    ) : FlightBaseDto(Id, CodeAirlinesIcao, OriginAirport, DestinationAirport, ScheduledDeparture);
}