//using AeroVeloz.Application.Models.flights;

//using AeroVeloz.Application.Repositories.Base;
//using AeroVeloz.Domain.Entities.Flight;

//namespace AeroVeloz.Application.Repositories.Flight
//{
//    public interface IFlightRepository : IBRepository<Flights, short>
//    {
//        // Filtro blindado por número + IATA (regla de Joel)
//        Task<FlightReadModel?> GetByFlightAndAirlineAsync(short flightNumber, string iataCode);

//        // JOIN para admins de aeropuerto
//        Task<FlightReadModel?> GetByFlightAndOrganizationAsync(short flightNumber, int orgId);

//        //  Vuelos activos por aerolínea
//        Task<IReadOnlyCollection<FlightReadModel>> GetActiveFlightsByAirlineAsync(string iataCode);

//        // sVuelos por organización
//        Task<IReadOnlyCollection<FlightReadModel>> GetFlightsByOrganizationAsync(int orgId);
//    }
//}




//EL MODEL QUE TENIAS CREADOS DEBES IMPLEMENTARLO EN DOMAIN ESO NO VA EN APPLICATION