using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Validators.Orquestador.Airline
{
    public interface IAirlineValidator
    {
        // Valida las reglas antes de procesar un lote de vuelos
        Task<ValidationResult> ValidateProcessBatchAsync(Entities.Airlines.Airline airline);
    }
}