using MediatR;

namespace AeroVeloz.Domain.Events.Aiport
{
    public sealed record AirportRegisteredDomainEvent(
        string? CodeAirportICAO,
        string? CodeAirportIATA,
        string? NameAirport,
        string? Country,
        string? City,
        string? EmailOrganization,
        string? DefaultUserName,
        string? DefaultPasswordHash,
        DateTime CreatedAt
    ) : INotification;
}
