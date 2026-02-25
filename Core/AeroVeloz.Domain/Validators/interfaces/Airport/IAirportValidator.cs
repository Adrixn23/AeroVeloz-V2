using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Airports;

namespace AeroVeloz.Domain.Validators.interfaces.Airports
{
    public interface IAirportValidator { 

        ValidationResult ValidateAirportRegistration(Airport airport);
        ValidationResult ValidateAirportCode(string airportCode); 
        ValidationResult ValidateAirlineConnection(string airportCode, string airlineCode, string apiToken); 
        ValidationResult ValidateAirportAccess(Guid userId, string airportCode); 
        ValidationResult ValidateApiKey(string airportCode, string apiKey); 
        ValidationResult ValidateAirportDeactivation(string airportCode); }
}
