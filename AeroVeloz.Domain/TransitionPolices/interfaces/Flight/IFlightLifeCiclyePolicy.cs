using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.TransitionPolices.interfaces.Flight
{
    public interface IFlightLifeCiclyePolicy
    {
        //descomentar cuando se cree el enum correspondiente
        public bool CanTrasition(/*FlightState from, FlightSate to*/);
    }
}
