using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Services.Interfaces.Airport;
using AeroVeloz.Domain.Validators.interfaces.Airports;

namespace AeroVeloz.Domain.Validators.Orquestador.Airport
{
    public class AirportValidator : IAirportValidator
    {
        private readonly IDomainServiceAirport domainServiceAirport;
    
        public AirportValidator() { 
        
        
        }

        public Task<ValidationResult> ValidateForCreateAirport(Entities.Organization.Airports.Airport airport)
        {
            throw new NotImplementedException();
        }
    }
}
