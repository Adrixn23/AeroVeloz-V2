namespace AeroVeloz.Domain.Events.Aiport
{
    public record AirportSuspendedDomainEvent(
         string? codeAirport,
         string? codeAiportIATA,
         bool isActive,
         Guid IdUserMaster
        ) : INotification
    
}
