using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
