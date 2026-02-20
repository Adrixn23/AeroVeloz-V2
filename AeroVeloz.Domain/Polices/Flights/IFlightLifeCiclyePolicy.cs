using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.Polices.Flights
{
    public interface IFlightLifeCiclyePolicy
    {
        //descomentar cuando se cree el enum correspondiente
        public bool CanTrasition(/*FlightState from, FlightSate to*/);
    }
}


