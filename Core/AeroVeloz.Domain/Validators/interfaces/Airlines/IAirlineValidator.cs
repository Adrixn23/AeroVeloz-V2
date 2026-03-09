using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Organization.Airlines;

namespace AeroVeloz.Domain.Validators.interfaces.Airlines
{
    public interface IAirlineValidator
    {


        Task<ValidationResult> ValidateCreateAsync(Airline airline);


        // Valida las reglas antes de procesar un lote de vuelos
        Task<ValidationResult> ValidateProcessBatchAsync(Airline airline);




      

    }
}