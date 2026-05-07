using MediatR;

namespace AeroVeloz.Domain.Events.Operations
{
  
    public sealed record ChangeDoorEvent(
        short FlightNumber,
        string? CodeAirline,
        string? BoardingGateDeparture,
        string? BoardingGateArrival,
        DateTime ChangedAt,
        string? Cause,
        Guid IdUser
    ) : INotification;
}
