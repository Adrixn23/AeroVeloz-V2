using MediatR;


namespace AeroVeloz.Domain.Events.EventsAirlines
{
 
        
          //referencia sad Seccion 3.5 - Componente de Auditora Inmutable
      public record FlightAuditEntryCreated(
          Guid EntityId,
           string ActorType, // "Airline", "OpsTeam", "System"
           string ActionDetail,
           string NewValuesJson,
           DateTime Timestamp
      ) : INotification;
    
}
