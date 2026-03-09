using System;

namespace AeroVeloz.Domain.Models.Flights
     {
        public sealed record FlightReadModel(
            short FlightNumber,
            string AirlineIataCode, 
           string Origin,
            string Destination,
            DateTime DepartureTime,
           byte FlightStatus,
           string? nameOrganization,
           int OrgId
     );
    }