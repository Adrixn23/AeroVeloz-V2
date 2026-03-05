using AeroVeloz.Domain.Common.Enums;
using MediatR;


namespace AeroVeloz.Domain.Events.EventsAirlines
{
   // Event Cambio de estado enviado directamente por la aerolnea.
         // Referencia sadd Pagg 22 punto 3.
         
    public record FlightStateChangedByAirline(
          string FlightNumber,
           FlightStateEnum NewState,
         string AirlineCode,
          DateTime UpdatedAt
       ) : INotification;
}
