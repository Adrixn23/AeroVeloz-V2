using MediatR;

namespace AeroVeloz.Domain.Events.EventsFlights
{
     public record FlightNew(
          // GUID Id
          string codeAirlines,
         string OriginAirport,
         DateTimeOffset ScheduledDeparture,
         DateTimeOffset ScheduledArrival
          
         ) : INotification;
    

    
}
