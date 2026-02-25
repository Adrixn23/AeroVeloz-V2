using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Entities.Organization.Airports;
using AeroVeloz.Domain.Validators.interfaces.Airports;
using AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.Aiport;

namespace AeroVeloz.Domain.Validators.Orquestador.Airport
{
    public class AirportValidator : IAirportValidator
    {
        
        public ValidationResult Validation(Entities.Organization.Airports.Airport airport)
        {
            var errors = new List<DomainError>();

            if (string.IsNullOrEmpty(airport.country))
                errors.Add(AiportErrors.InvalidCountryAirport);
            if (string.IsNullOrEmpty(airport.nameAirport))
                errors.Add(AiportErrors.InvalidNameAiport);
            if (string.IsNullOrEmpty(airport.emailAirport) || !airport.emailAirport.Contains("@"))
                errors.Add(AiportErrors.InvalidEmailAirport);
            if (string.IsNullOrEmpty(airport.city))
                errors.Add(AiportErrors.InvalidCityAirport);

            var result = new ValidationResult();

            return result.Failur(errors);
        }
    }
}
