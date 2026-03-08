using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Services.Interfaces.Airport;
using AeroVeloz.Domain.Validators.interfaces.Airport;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Aiport;

namespace AeroVeloz.Domain.Validators.Orquestador.Airport
{
    public class ConnectionAiportAirline : IConnectionAiportAirline
    {

        private readonly IDomainServiceAirport _domainServiceAirport;

        public ConnectionAiportAirline(IDomainServiceAirport domainServiceAirport)
        {
            _domainServiceAirport = domainServiceAirport;
        }

        public async Task<ValidationResult> ValidationForCreateConnectionAirlineByAirport(ConectionsAirlineAirport contections)
        {
            var errors = new List<ErrosValidationResults>();

            if (contections == null)
            {
                errors.Add(ConnectionAirportErrors.ConnectionInvalidObject);
                return new ValidationResult().Failur(errors);
            }

            if (string.IsNullOrWhiteSpace(contections.codeAirlines))
                errors.Add(ConnectionAirportErrors.ConnectionMissingAirlineCode);

            if (string.IsNullOrWhiteSpace(contections.codeAirport))
                errors.Add(ConnectionAirportErrors.ConnectionMissingAirportCode);

            if (errors.Any())
                return new ValidationResult().Failur(errors);

            var exists = await _domainServiceAirport.AirportHasAirlineConnectionAsync(contections.codeAirport!, contections.codeAirlines!);
            if (exists)
                errors.Add(ConnectionAirportErrors.ConnectionAlreadyExists);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }
    }
}
