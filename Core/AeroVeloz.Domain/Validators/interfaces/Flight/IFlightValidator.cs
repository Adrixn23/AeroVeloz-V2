using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flight;
using System.Threading.Tasks;


namespace AeroVeloz.Domain.Validators.interfaces.Flight
{
    public interface IFlightValidator
    {

        // Valida todas las reglas de negocio antes de crear un vuelo
 
        Task<ValidationResult> ValidateCreateAsync(Flights flight);

       Task<ValidationResult> ValidateFlightRowAsync(Flights flight);



        // Valida las reglas antes de actualizar el estado de un vuelo
        Task<ValidationResult> ValidateStateTransitionAsync(Flights flight);


      
        
    }
}
