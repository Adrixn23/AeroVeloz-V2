<<<<<<< HEAD
﻿
using AeroVeloz.Domain.Models.Flights;
using AeroVeloz.Domain.Entities.Flights;

using AeroVeloz.Application.DTOs.Flights.Base;

namespace AeroVeloz.Application.Repositories.Flights
{
    public interface IFlightRepository : IBRepository<Flight, short>
    {
        // Filtro blindado por número + IATA (regla Joel)
        Task<FlightReadModel?> GetByFlightAndAirlineAsync(short flightNumber, string iataCode);

        

        //  Vuelos activos por aerolínea
        Task<IReadOnlyCollection<FlightReadModel>> GetActiveFlightsByAirlineAsync(string iataCode);

      
    }
}




=======
﻿using AeroVeloz.Application.DTOs.Flights;
using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Flights;

namespace AeroVeloz.Application.Repositories.Flights
{
    public interface IFlightRepository : IBRepository<Flight>
    {
        Task<FlightReadDto?> GetByFlightNumberAndAirlineAsync(short flightNumber, string codeAirlines);
        Task<IReadOnlyCollection<FlightReadDto>> GetActiveFlightsByAirlineAsync(string codeAirlines);
        Task<IReadOnlyCollection<FlightReadDto>> GetPublicActiveFlightsAsync();
        Task<IReadOnlyCollection<FlightReadDto>> GetPublicFlightsByAirportAsync(string airportCode);
        Task<bool> ExistsFlightAsync(short flightNumber, string codeAirlines);
        Task<Flight?> GetEntityByNumberAndAirlineAsync(short flightNumber, string codeAirlines);
        Task<bool> PersistBatchAsync(IEnumerable<Flight> flights);
        Task<bool> UpdateFlightStateAsync(short flightNumber, string codeAirlines, byte newStateId);
        Task<bool> HasActiveConnectionAsync(string codeAirlines, string airportCode);
    }
}
>>>>>>> origin/testeo-branch
