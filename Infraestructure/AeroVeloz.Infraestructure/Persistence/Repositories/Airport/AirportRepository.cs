using AeroVeloz.Application.Repositories.Airport;
using AeroVeloz.Domain.Models.Airports;
using AeroVeloz.Domain.Services.Interfaces.Airport;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Airport
{
    public class AirportRepository : IAirportRepository, IDomainServiceAirport
    {
        public Task<bool> AirportHasAirlineConnectionAsync(string airportCode, string airlineCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAirportByOrganizations(string? codeIata, string? codeIacao)
        {
            throw new NotImplementedException();
        }

        public Task<AirportModel> GetAirportByCode(string? codeAirport)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<AirportModel>> GetAllAirport()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(Domain.Entities.Organization.Airports.Airport entity)
        {
            throw new NotImplementedException();
        }
    }
}
