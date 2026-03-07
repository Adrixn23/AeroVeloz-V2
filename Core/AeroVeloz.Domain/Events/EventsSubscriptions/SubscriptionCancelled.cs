
using MediatR;

namespace AeroVeloz.Domain.Events.EventsSubscriptions
{
    // Ocurre cuando un usuario solicita ser removido del seguimiento operativo
    public record SubscriptionCancelled(
        Guid SubscriptionId,
        short FlightId,
      DateTime CancelledAt,
      string AirlineCode,
        string Reason
        ) : INotification;


}