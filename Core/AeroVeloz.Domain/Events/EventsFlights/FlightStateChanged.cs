using AeroVeloz.Domain.Common.Enums;
using MediatR;

namespace AeroVeloz.Domain.Events.EventsFlights
{
     public record FlightStateChanged(
          // GUID Id
          string codeAirlines,
         FlightStateEnum fromFlightState,
         FlightStateEnum toFlightState,
         string OriginAirport,
         DateTimeOffset ScheduledDeparture,
         DateTimeOffset ScheduledArrival
          
         ) : INotification;
    

    
}
