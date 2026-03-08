using MediatR;

namespace AeroVeloz.Domain.Events.Aiport
{
    public sealed record AirportSuspendedDomainEvent(
        string? CodeAirportICAO,
        string? CodeAirportIATA,
        string? NameAirport,
        Guid IdUserResponsible,
        DateTime SuspendedAt
    ) : INotification;
}
