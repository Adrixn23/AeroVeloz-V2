namespace AeroVeloz.Domain.Validators.interfaces.Airports
{
<<<<<<< HEAD
    public interface IAirportValidator
    {

        
    }


=======
    public interface IAirportValidator { 

        ValidationResult ValidateAirportRegistration(Airport airport);
        ValidationResult ValidateAirportCode(string airportCode); 
        ValidationResult ValidateAirlineConnection(string airportCode, string airlineCode, string apiToken); 
        ValidationResult ValidateAirportAccess(Guid userId, string airportCode); 
        ValidationResult ValidateApiKey(string airportCode, string apiKey); 
        ValidationResult ValidateAirportDeactivation(string airportCode); }
>>>>>>> 122bf176a5ed04e6f77387ce809b47f1237f8f65
}
