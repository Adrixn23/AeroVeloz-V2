using System;
using System.Collections.Generic;
using System.Text;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.ValidationBase;

namespace AeroVeloz.Domain.TransitionPolices
{
    public interface INotificationPolicy
    {

        //descomentar cuando se cree la clase vuelos y el enum de operationalChange
        public ValidationResult ShouldNotify(OperationalChange change, Flight flight); // deberia notificar??, depende el cambio se notifica

        public ValidationResult IsRecipientAllowed(Guid flightId /*, Subscription sub*/); // es para saber si a tal subscripcion se le permite mandar notificacion de este vuelo. 


    }
}
