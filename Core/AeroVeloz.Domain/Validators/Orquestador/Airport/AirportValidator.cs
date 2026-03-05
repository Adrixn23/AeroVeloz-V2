using AeroVeloz.Domain.Validators.interfaces.Airports;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.Airport;
using System.Text.RegularExpressions;

namespace AeroVeloz.Domain.Validators.Orquestador.Airport
{
    public class AirportValidator : IAirportValidator
    {
        private readonly Regex _emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        private readonly Regex _airportCodeRegex = new Regex(@"^[A-Z]{4}$");

        public ValidationResult ValidateAirportRegistration(AeroVeloz.Domain.Entities.Airports.Airport  airport)
        {
            var errors = new List<DomainError>();

            if (airport == null)
            {
                errors.Add(AirportErrors.AirportNotFound);
                return new ValidationResult().Failur(errors);
            }

            if (string.IsNullOrWhiteSpace(airport.Id) || !_airportCodeRegex.IsMatch(airport.Id))
                errors.Add(AirportErrors.InvalidAirportCode);

            if (string.IsNullOrWhiteSpace(airport.nameAirport))
                errors.Add(AirportErrors.AirportNameRequired);
            else if (airport.nameAirport.Length > 150)
                errors.Add(AirportErrors.MaxNameLength);

            if (string.IsNullOrWhiteSpace(airport.city))
                errors.Add(AirportErrors.CityRequired);

            
            if (string.IsNullOrWhiteSpace(airport.country))
                errors.Add(AirportErrors.CountryRequired);

           
            if (string.IsNullOrWhiteSpace(airport.emailAirport) || !_emailRegex.IsMatch(airport.emailAirport))
                errors.Add(AirportErrors.InvalidEmailFormat);

           
            if (string.IsNullOrWhiteSpace(airport.apiKeyMaster) || airport.apiKeyMaster.Length < 32)
                errors.Add(AirportErrors.InvalidApiKey);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateAirportCode(string airportCode)
        {
            var errors = new List<DomainError>();

            if (string.IsNullOrWhiteSpace(airportCode) || !_airportCodeRegex.IsMatch(airportCode))
                errors.Add(AirportErrors.InvalidAirportCode);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateAirlineConnection(string airportCode, string airlineCode, string apiToken)
        {
            var errors = new List<DomainError>();

            if (string.IsNullOrWhiteSpace(airportCode) || !_airportCodeRegex.IsMatch(airportCode))
                errors.Add(AirportErrors.InvalidAirportCode);

            if (string.IsNullOrWhiteSpace(airlineCode) || airlineCode.Length != 3)
                errors.Add(AirportErrors.InvalidApiKey);

            if (string.IsNullOrWhiteSpace(apiToken) || apiToken.Length < 20)
                errors.Add(AirportErrors.InvalidApiKey);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateAirportAccess(Guid userId, string airportCode)
        {
            var errors = new List<DomainError>();

            if (userId == Guid.Empty)
                errors.Add(AirportErrors.AirportNotFound);

            if (string.IsNullOrWhiteSpace(airportCode) || !_airportCodeRegex.IsMatch(airportCode))
                errors.Add(AirportErrors.InvalidAirportCode);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateApiKey(string airportCode, string apiKey)
        {
            var errors = new List<DomainError>();

            if (string.IsNullOrWhiteSpace(airportCode) || !_airportCodeRegex.IsMatch(airportCode))
                errors.Add(AirportErrors.InvalidAirportCode);

            if (string.IsNullOrWhiteSpace(apiKey) || apiKey.Length < 32)
                errors.Add(AirportErrors.InvalidApiKey);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateAirportDeactivation(string airportCode)
        {
            var errors = new List<DomainError>();

            if (string.IsNullOrWhiteSpace(airportCode) || !_airportCodeRegex.IsMatch(airportCode))
                errors.Add(AirportErrors.InvalidAirportCode);

            // errors.Add(AirportErrors.AirportHasActiveFlights);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }
    }
}
