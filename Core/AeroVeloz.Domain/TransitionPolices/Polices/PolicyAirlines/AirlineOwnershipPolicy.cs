using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline;
using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AeroVeloz.Domain.TransitionPolices.Polices.PolicyAirlines
{
    class AirlineOwnershipPolicy : IAirlineOwnershipPolicy
    {
        public bool IsAirlineOwnerOfBatch(string Airlinecode, IEnumerable<Flight> batch)
        {

           // Si el lote es nulo o viene vacío, devolvemos falso.
             // Una aerolínea no puede procesar "la nada
             if (Airlinecode == null || !batch.Any())
            {
                return false;
            }
            // Si encuentra tan solo UN vuelo cuyo codeAirlines sea diferente al airlineCode, devuelve false
            return batch.All(f => f.codeAirlines == Airlinecode);
        }
    }
}
