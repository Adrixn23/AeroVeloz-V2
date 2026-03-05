using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AeroVeloz.Domain.ValidationBase;
using Flight = AeroVeloz.Domain.Entities.Flight.Flight;

namespace AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline

{
    public interface IAirlineStatePolicy
    { 
 public ValidationResult EvaluateStateTransition(Flight flight, FlightStateEnum newState);

    }
}
