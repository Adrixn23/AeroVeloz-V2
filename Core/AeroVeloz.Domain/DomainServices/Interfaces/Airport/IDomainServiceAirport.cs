using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Airport;

namespace AeroVeloz.Domain.Services.Interfaces.Airport
{
    public interface IDomainServiceAirport
    {
        Task<ValidationResult> RegisterAirportAsync(string code, string name, string city, string country, string email);
        Task<bool> ValidateAirportCodeAsync(string airportCode);
        Task<bool> IsAirportActiveAsync(string airportCode);
        Task<bool> CanUserManageAirportAsync(Guid userId, string airportCode);
        Task<IEnumerable<string>> GetUserManagedAirportsAsync(Guid userId);
        Task<bool> ValidateAirlineConnectionAsync(string airportCode, string airlineCode);
        Task<ValidationResult> EstablishAirlineConnectionAsync(string airportCode, string airlineCode, string apiToken);
        Task<bool> IsFlightAuthorizedForAirportAsync(int flightNumber, string airportCode);
        Task<IEnumerable<Airport>> GetAirportsByCountryAsync(string country);
        Task<bool> ValidateApiKeyAsync(string airportCode, string apiKey);
    }
}
