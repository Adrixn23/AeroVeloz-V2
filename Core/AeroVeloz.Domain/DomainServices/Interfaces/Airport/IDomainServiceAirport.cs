using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Users;

namespace AeroVeloz.Domain.Services.Interfaces.Airport
{
    public interface IDomainServiceAirport
    {
        Task<bool> ValidateAirportCodeAsync(string airportCode);
        Task<bool> IsAirportActiveAsync(string airportCode);
        Task<bool> CanUserManageAirportAsync(Guid userId, string airportCode);
        Task<IEnumerable<User>> GetUserManagedAirportsAsync(Guid userId);
        Task<IEnumerator<User>> GetAllUserAiportAsync(string aiportCode);
        Task<bool> ValidateAirlineConnectionAsync(string airportCode, string airlineCode);
        Task<ValidationResult> EstablishAirlineConnectionAsync(string airportCode, string airlineCode, string apiToken);
        Task<bool> IsFlightAuthorizedForAirportAsync(int flightNumber, string airportCode);
        Task<IEnumerable<Airline>> GetAirlinesAsyncAirport();
        Task<IEnumerable<AeroVeloz.Domain.Entities.Airports.Airport>> GetAirportsByCountryAsync(string country);
        Task<bool> ValidateApiKeyAsync(string airportCode, string apiKey);
        Task<IEnumerator<User>> BatchLoadUsersAirportAsync(IEnumerable<User> users);
        
     
    }
}
