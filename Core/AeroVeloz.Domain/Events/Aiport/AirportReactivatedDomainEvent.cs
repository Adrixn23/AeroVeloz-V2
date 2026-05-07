using MediatR;

namespace AeroVeloz.Domain.Events.Aiport
{
    public sealed record AirportReactivatedDomainEvent(
        string? CodeAirportICAO,
        string? CodeAirportIATA,
        string? NameAirport,
        Guid IdUserResponsible,
        DateTime ReactivatedAt
    ) : INotification;
}
