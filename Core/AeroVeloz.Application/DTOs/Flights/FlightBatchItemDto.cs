namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightBatchItemDto(
        short Id, // Número de vuelo
        string? codeAirlinesIcao,
        string? OriginAirport,
        string? DestinationAirport,
        DateTimeOffset ScheduledDeparture,
        string? BoardingGate,
        string? BoardingGateArrived
    ) : FlightBaseDto(Id, codeAirlinesIcao, OriginAirport, DestinationAirport, ScheduledDeparture);
}