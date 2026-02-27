using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.TransitionPolices.Polices.PolicyAirlines
{
    class BatchCoherencePolicy : IBatchCoherencePolicy
    {
        public bool IsBatchCoherent(IEnumerable<Flight> batch, string airportName)
        {
            if (batch == null || !batch.Any()) return false;


            return batch.All(f => f.OriginAirport == airportName || f.DestinationAirport == airportName); 

           
        }
    }
}
