using MediatR;

namespace AeroVeloz.Domain.Events.Aiport
{
   
    public sealed record AirportConnectionDeactivatedDomainEvent(
        Guid ConnectionId,
        string? CodeAirport,
        string? CodeAirline,
        Guid IdUserResponsible,
        DateTime DeactivatedAt
    ) : INotification;
}
