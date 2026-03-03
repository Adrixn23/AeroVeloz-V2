using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Domain.ValidationBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.TransitionPolices
{
    public interface INotificationPolicy
    {

        //descomentar cuando se cree la clase vuelos y el enum de operationalChange
        public ValidationResult ShouldNotify(OperationChange change, Flight flight); // deberia notificar??, depende el cambio se notifica

        // es para saber si a tal subscripcion se le permite mandar notificacion de este vuelo. 
        // El timestamp para evaluar la regla estricta de 15 minutos(SLA)
        ValidationResult IsRecipientAllowed(Guid flightId, Subscription subscription, DateTimeOffset flightStatusChangedAt);
    }
}
