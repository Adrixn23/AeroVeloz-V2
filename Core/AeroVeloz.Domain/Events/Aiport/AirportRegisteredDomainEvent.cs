using AeroVeloz.Domain.TransitionPolices;

namespace AeroVeloz.Domain.Events.Aiport
{
    public record AirportRegisteredDomainEvent(
        string? codeAirport,
        string? codeAirportIATA,
        string? nameAiport,
        string? apiKeyMaster,
        string? createAt,
        string? emailOrganization
        ) : INotification;
  
}
