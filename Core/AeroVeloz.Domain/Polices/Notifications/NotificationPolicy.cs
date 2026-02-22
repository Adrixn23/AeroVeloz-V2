using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.Polices.Notifications
{
    public class NotificationPolicy : INotificationPolicy
    {

        //agregar campos/atributos cuando se modifique lo descripto en la interfaz

        public bool IsRecipientAllowed(Guid flightId)
        {
            throw new NotImplementedException(); //agregar logica de negocio 
        }

        //agregar campos/atributos cuando se modifique lo descripto en la interfaz

        public bool ShouldNotify()
        {
            throw new NotImplementedException(); //agregar logica de negocio 
        }
    }
}
