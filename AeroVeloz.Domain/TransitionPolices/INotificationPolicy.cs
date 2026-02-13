using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.TransitionPolices
{
    public interface INotificationPolicy
    {

        //descomentar cuando se cree la clase vuelos y el enum de operationalChange
        public bool ShouldNotify(/*OperationalChange change, Flight: flight */);
    }
}
