<<<<<<< HEAD
﻿namespace AeroVeloz.Domain.Services.Interfaces.Airport
{
    public interface IDomainServiceAirport
    {
        //estos metodos permiten validar si el aeropuerto que se esta intando crear ya existe dentro de la
        //organizacion  y tambien permite obtener las connnections que tiene el aeropuerto con x aerolinas
        //validando si ya la connecition existe o no existe. 

        Task<bool> ExistAirportByOrganizations(string? codeIata, string? codeIacao);
        Task<bool> AirportHasAirlineConnectionAsync(string airportCode, string airlineCode);

=======
﻿using AeroVeloz.Domain.Common.Enums;
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
        
     
>>>>>>> 122bf176a5ed04e6f77387ce809b47f1237f8f65
    }
}
