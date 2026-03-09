namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightReadDto(
        short FlightNumber,
        string? CodeAirlines,
        string? OriginAirport,
        string? DestinationAirport,
        DateTimeOffset ScheduledDeparture,
        string? BoardingGate,
        string? BoardingGateArrived,
        byte FlightStateId,
        string? FlightStateName
    );
}
