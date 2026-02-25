using MediatR;

namespace AeroVeloz.Domain.Events.Aiport
{
    public record AirportReactivatedDomainEvent(
         string? codeAirport,
         string? codeAiportIATA,
         bool isActive,
         Guid IdUserMaster
        ) : INotification;
    
}
