using MediatR;

namespace AeroVeloz.Domain.Events.Aiport
{

    public sealed record AirportConnectionCreatedDomainEvent(
        Guid ConnectionId,
        string? CodeAirport,
        string? CodeAirline,
        Guid IdUserResponsible,
        DateTime CreatedAt
    ) : INotification;
}
