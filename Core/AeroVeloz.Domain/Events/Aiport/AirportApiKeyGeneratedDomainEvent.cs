using MediatR;

namespace AeroVeloz.Domain.Events.Aiport
{
  
    public sealed record AirportApiKeyGeneratedDomainEvent(
        string? CodeAirportICAO,
        string? CodeAirportIATA,
        string? NameAirport,
        Guid IdUserResponsible,
        DateTime GeneratedAt
    ) : INotification;
}
