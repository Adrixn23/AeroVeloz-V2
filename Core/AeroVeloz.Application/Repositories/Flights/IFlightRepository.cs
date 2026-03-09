
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




