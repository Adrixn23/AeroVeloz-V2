using System;

namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightBatchItemDto(
        string? codeAirlinesIcao,
        string? OriginAirport,
        string? DestinationAirport,
        DateTimeOffset ScheduledDeparture,
        string? BoardingGate,
        string? BoardingGateArrived
    );
}
