using AeroVeloz.Domain.Common.codeError.codeErrorAirlines;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Validators.interfaces.Airlines;

namespace AeroVeloz.Domain.Validators.Orquestador.Airlines
{
    public class AirlineValidatorImpl : IAirlineValidator
    {
        public Task<ValidationResult> ValidateCreateAsync(Airline airline)
        {
            var errors = new List<ErrosValidationResults>();

            if (string.IsNullOrWhiteSpace(airline.codeAirlinesIcao))
                errors.Add(ErrorAirlines.InvalidAirlineCode);

            if (string.IsNullOrWhiteSpace(airline.codeIata))
                errors.Add(ErrorAirlines.MissingIataCode);
            else if (airline.codeIata.Length < 3)
                errors.Add(ErrorAirlines.InvalidIataFormat);

            if (errors.Count > 0)
                return Task.FromResult(new ValidationResult().Failur(errors));

            return Task.FromResult(new ValidationResult().Success());
        }

        public Task<ValidationResult> ValidateProcessBatchAsync(Airline airline)
        {
            var errors = new List<ErrosValidationResults>();

            if (string.IsNullOrWhiteSpace(airline.codeAirlinesIcao))
                errors.Add(ErrorAirlines.InvalidAirlineCode);

            if (string.IsNullOrWhiteSpace(airline.codeIata))
                errors.Add(ErrorAirlines.MissingIataCode);

            if (errors.Count > 0)
                return Task.FromResult(new ValidationResult().Failur(errors));

            return Task.FromResult(new ValidationResult().Success());
        }
    }
}
