using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Application.Models.flights
{
     public sealed class FlightReadModel
    {
        // identificacion d e ruta
        public short FlightNumber { get; init; }
        public string AirlineCode { get; init; } = string.Empty;


        // Ruta
        public string OriginAirport { get; init; } = string.Empty;

        public string DestinationAirport { get; init} = string.Empty;

        // Tiempos
        public DateTime ScheduledDeparture { get; init; }
        public DateTime ScheduledArrival { get; init; }

        // estado visible para el pasajero
        public string FlightStatus { get; init; } = string.Empty;
        public string? BoardingGate { get; init; } // el ? por que el vuelo puede ta programado pero pued eestar sin puerta a asignar

    }
}
