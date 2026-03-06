using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flight;
using System.Threading.Tasks;


namespace AeroVeloz.Domain.Validators.interfaces.Flight
{
    public interface IFlightValidator
    {

        // Valida todas las reglas de negocio antes de crear un vuelo
 
        Task<ValidationResult> ValidateCreateAsync(Entities.Flight.Flights flight);

       Task<ValidationResult> ValidateFlightRowAsync(Entities.Flight.Flights flight);



        // Valida las reglas antes de actualizar el estado de un vuelo
        Task<ValidationResult> ValidateStateTransitionAsync(Entities.Flight.Flights flight);


      
        
    }
}
