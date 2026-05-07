using AeroVeloz.Application.DTOs.Flights;

public record FlightSaveDto(
    short Id,
    string? codeAirlinesIcao,
    string? OriginAirport,
    string? DestinationAirport,
    DateTimeOffset ScheduledDeparture,
    byte FlightStateId,
    string? BoardingGate,
    string? BoardingGateArrived
) : FlightBaseDto(Id, codeAirlinesIcao, OriginAirport, DestinationAirport, ScheduledDeparture);