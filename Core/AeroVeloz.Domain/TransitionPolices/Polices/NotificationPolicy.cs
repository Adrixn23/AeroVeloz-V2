using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.TransitionPolices;
using AeroVeloz.Domain.ValidationBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.Notifications
{
    public class NotificationPolicy : INotificationPolicy
    {

        //agregar campos/atributos cuando se modifique lo descripto en la interfaz
        public ValidationResult ShouldNotify(OperationalChange change, Flight flight)
        {
            throw new NotImplementedException();
        }

        //agregar campos/atributos cuando se modifique lo descripto en la interfaz
        ValidationResult INotificationPolicy.IsRecipientAllowed(Guid flightId)
        {
            throw new NotImplementedException();
        }
    }
}
