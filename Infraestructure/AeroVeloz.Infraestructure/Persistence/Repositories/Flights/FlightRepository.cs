
//using AeroVeloz.Application.Models.flights;
//using AeroVeloz.Application.Repositories.Flight;
//using AeroVeloz.Domain.Repositories;
//using AeroVeloz.Infraestructure.Persistence.context;

//namespace AeroVeloz.Infraestructure.Persistence.Repositories.Flights
//{
//    public class FlightRepository : IFlightRepository, IFlightDomainRepository // tanto la interfaz de iflightrepository y iflightdomainService,
//                                                                              // pasar por inyeccion de dependencias las politicas y las interfaces de las politicas
//    {
//        private readonly AeroVelozDbContext _context;

//        public FlightRepository(AeroVelozDbContext context)
//        {
//            _context = context;
//        }

//        public Task<bool> CreateEntity(Domain.Entities.Flight.Flights entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> DeleteEntity(Domain.Entities.Flight.Flights entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> ExistsFlightAsync(short flightNumber, string airlineCode)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IReadOnlyCollection<FlightReadModel>> GetActiveFlightsByAirlineAsync(string iataCode)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<FlightReadModel?> GetByFlightAndAirlineAsync(short flightNumber, string iataCode)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<FlightReadModel?> GetByFlightAndOrganizationAsync(short flightNumber, int orgId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IReadOnlyCollection<FlightReadModel>> GetFlightsByOrganizationAsync(int orgId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> IsAirlineOwnerOfFlightAsync(short flightNumber, string airlineCode)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> IsOrganizationActiveAsync(int idOrganization)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> IsOriginAirportActiveAsync(string airportCode)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> UpdateEntity(Domain.Entities.Flight.Flights entity)
//        {
//            throw new NotImplementedException();
//        }
       
//    }
//}