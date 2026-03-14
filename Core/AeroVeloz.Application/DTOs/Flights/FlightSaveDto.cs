using AeroVeloz.Application.DTOs.Flights;

public record FlightSaveDto(
    short Id,
    string? CodeAirlinesIcao,
    string? OriginAirport,
    string? DestinationAirport,
    DateTimeOffset ScheduledDeparture,
    byte FlightStatesId,
    string? BordingGate,
    string? BoardingGateArrived
) : FlightBaseDto(Id, CodeAirlinesIcao, OriginAirport, DestinationAirport, ScheduledDeparture);