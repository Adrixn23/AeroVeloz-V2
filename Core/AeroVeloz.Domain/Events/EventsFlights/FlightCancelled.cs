using MediatR;


namespace AeroVeloz.Domain.Events.EventsFlights
{
     public record FlightCancelled (
                  // GUID id user
          string codeAirlines,
          string OriginAirport,
          string DestinationAirport,
          DateTimeOffset ScheduledDeparture,
          DateTimeOffset ScheduledArrival
         ) : INotification;




}
