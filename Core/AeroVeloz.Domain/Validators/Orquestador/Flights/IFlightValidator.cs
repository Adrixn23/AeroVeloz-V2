using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flight;


namespace AeroVeloz.Domain.Validators.Orquestador.Flight
{
    public interface IFlightValidator
    {

        // Valida todas las reglas de negocio antes de crear un vuelo
        Task<ValidationResult> ValidateCreateAsync(Flights Flight);




        // Valida las reglas antes de actualizar el estado de un vuelo
        Task<ValidationResult> ValidateUpdateStatusAsync(Flights Flight);
    }
}
