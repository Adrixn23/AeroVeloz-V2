using MediatR;
using System;
namespace AeroVeloz.Domain.Events.EventsAirlines
{
    public record FlightBatchRejected(
         string AirlineCode,
         string Reason,
         DateTime RejectedAt
        ) : INotification;

   

       
    
}
