using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flights;
using System.Threading.Tasks;


namespace AeroVeloz.Domain.Validators.interfaces.Flight
{
    public interface IFlightValidator
    {

        // Valida todas las reglas de negocio antes de crear un vuelo
 
       Task<ValidationResult> ValidateCreateAsync(Entities.Flights.Flight flight);

       Task<ValidationResult> ValidateFlightRowAsync(Entities.Flights.Flight flight);

       // Valida las reglas antes de actualizar el estado de un vuelo
       Task<ValidationResult> ValidateStateTransition(Entities.Flights.Flight flight);

      
        
    }
}
