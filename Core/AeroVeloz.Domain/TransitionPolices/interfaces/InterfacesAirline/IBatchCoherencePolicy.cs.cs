using AeroVeloz.Domain.Entities.Flight;
using System;
using System.Collections.Generic;
using AeroVeloz.Domain.ValidationBase;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline
{
    public interface IBatchCoherencePolicy
    {
        ValidationResult IsBatchCoherent(IEnumerable<Flight> batch, string airportName);


        /// Evalúa si un lote completo de vuelos es coherente con el aeropuerto que lo recibe.
        /// Retornará true solo si TODOS los vuelos tienen este aeropuerto como origen o destino.

    }
}
