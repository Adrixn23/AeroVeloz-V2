using System;
using System.Collections.Generic;

namespace AeroVeloz.Application.DTOs.Flights
{
    public class FlightListDto
    {
        public short Id { get; set; }
        public string? CodeAirlineIcao { get; set; }
        public string? FlightNumber { get; set; } // Representado por el Id o un prop
        public string? OriginAirport { get; set; }
        public string? DestinationAirport { get; set; }
        public DateTimeOffset ScheduledDeparture { get; set; }
        public string? BordingGate { get; set; }
        public byte FlightStateId { get; set; }
    }
}
