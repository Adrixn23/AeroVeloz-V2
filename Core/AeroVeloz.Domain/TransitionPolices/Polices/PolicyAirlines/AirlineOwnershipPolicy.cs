using AeroVeloz.Domain.Entities.Flight; // Para reconocer "Flight"
using AeroVeloz.Domain.ValidationBase;   // Para reconocer "ValidationResult"
    using AeroVeloz.Domain.Validators.codeError.codeError_Airlines; // Para reconocer "ErrorAirlines"
    using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline; // Para reconocer la Interfaz
    using System.Collections.Generic;
    using System.Linq;
namespace AeroVeloz.Domain.TransitionPolices.Polices.PolicyAirlines
{
    class AirlineOwnershipPolicy : IAirlineOwnershipPolicy
    {
        // Evalúa si el código de aerolínea proporcionado es el dueño legítimo de todos los vuelos en el lote
        public ValidationResult IsAirlineOwnerOfBatch(string Airlinecode, IEnumerable<Flight> batch)
        {
            var result = new ValidationResult();


            // No se permite procesar lotes nulos o codigos de aerolínea vacioos
            if (string.IsNullOrEmpty(Airlinecode) || batch == null || !batch.Any())
            {
                return result.Failur(ErrorAirlines.InvalidAirlineCode);
            }
            // Si encuentra tan solo UN vuelo cuyo codeAirlines sea diferente al airlineCode, devuelve false
            if (!batch.All(f => f.codeAirlines == Airlinecode))
                 {
                // Error Airline_05: "La aerolinea no es dueña de este lote de vuelos, o este lote ya fue procesado
                return result.Failur(ErrorAirlines.InvalidUnauthorizedBatchAccess);
                   }
           // El lote es íntegro y pertenece a la aerolinea
            return result.Success();

        }


        
    }
}
