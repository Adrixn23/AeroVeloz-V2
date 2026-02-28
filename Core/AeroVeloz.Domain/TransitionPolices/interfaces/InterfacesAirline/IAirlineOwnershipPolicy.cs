using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroVeloz.Domain.Entities.Flight;
using System.Collections.Generic;
using Flight = AeroVeloz.Domain.Entities.Flight.Flight;
using AeroVeloz.Domain.ValidationBase;
namespace AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline
{
    public interface IAirlineOwnershipPolicy
    {
        /// Evalúa si la aerolínea que realiza la petición es la dueña legítima de todos los vuelos del lote.
        /// Retornará true solo si el código de la aerolínea coincide en cada vuelo.
        ValidationResult IsAirlineOwnerOfBatch(string Airlinecode, IEnumerable<Flight> batch);

    


}
}
