using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Validators.interfaces.Airlines
{
    public interface IAirlineValidator
    {


        Task<ValidationResult> ValidateCreateAsync(Entities.Airlines.Airline airline);


        // Valida las reglas antes de procesar un lote de vuelos
        Task<ValidationResult> ValidateProcessBatchAsync(Entities.Airlines.Airline airline);




      

    }
}