using MediatR;

namespace AeroVeloz.Domain.Events.EventsAirlines
{
    public record FlightBatchRejected(
         string AirlineCode,
         string Reason,
         DateTime RejectedAt
        ) : INotification;

   

       
    
}
