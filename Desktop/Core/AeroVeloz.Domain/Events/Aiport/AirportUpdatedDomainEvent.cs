using MediatR;

namespace AeroVeloz.Domain.Events.Aiport
{
 
    public sealed record AirportUpdatedDomainEvent(
        string? CodeAirportICAO,
        string? CodeAirportIATA,
        string? NameAirport,
        string? Country,
        string? City,
        Guid IdUserResponsible,
        DateTime UpdatedAt
    ) : INotification;
}
