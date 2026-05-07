using AeroVeloz.Domain.Common.Enums;
using MediatR;

namespace AeroVeloz.Domain.Events.EventsSubscriptions
{
    // Evento de dominio: Emitido cuando un pasajero o visitante es agregado exitosamente a la lista de  seguimiento.

    public record SubscriptionCreated(
        // Guid user recuerda esto xd
        Guid SubscriptionId,
       short FlightId,
       string AirlineCode,
          string ContactValue,
          SubscriptionChannel Channel,
            DateTime CreatedAt

        ) : INotification;
   
   
}
