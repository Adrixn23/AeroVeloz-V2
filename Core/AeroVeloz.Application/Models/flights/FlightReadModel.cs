using AeroVeloz.Domain.Common.Enums;

namespace AeroVeloz.Application.Models.flights
{
    public sealed record FlightReadModel(
        short Id,                          // viene de BEntity<short>
        string? CodeAirlines,              // codeAirlines
        string FlightCode,                 // CodeAirlines + Id ej: "IB3421"
        string? OriginAirport,             // OriginAirport
        string? DestinationAirport,        // DestinationAirport
        DateTimeOffset ScheduledDeparture, // ScheduledDeparture
        DateTimeOffset ScheduledArrival,   // ScheduledArrival
        string? BoardingGate,              // BoardingGate
        string? BoardingGateArrived,       // BoardingGateArrived
        FlightStateEnum FlightState        // FlightStated
    );
}