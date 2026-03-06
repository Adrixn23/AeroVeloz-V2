using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Models.Airports;
namespace AeroVeloz.Application.Repositories.Airport
{
    public  interface IAirportRepository : IBRepository<Domain.Entities.Organization.Airports.Airport, string>
    {
        Task<IReadOnlyCollection<AirportModel>> GetAllAirport();
        Task<AirportModel> GetAirportByCode(string? codeAirport);
    }
}
