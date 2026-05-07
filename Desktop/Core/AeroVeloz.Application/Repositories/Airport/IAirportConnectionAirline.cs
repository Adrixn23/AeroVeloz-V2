using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Models.Airports;

namespace AeroVeloz.Application.Repositories.Airport
{
    public interface IAirportConnectionAirline : IBRepository<ConectionsAirlineAirport>
    {
        Task<IReadOnlyCollection<AirlineConnectionByAirportModel>> GetAirportConnectionById(string? codeAirportIcao);
        Task<ConectionsAirlineAirport?> GetConnectionByIdAsync(Guid connectionId);
    }
}
