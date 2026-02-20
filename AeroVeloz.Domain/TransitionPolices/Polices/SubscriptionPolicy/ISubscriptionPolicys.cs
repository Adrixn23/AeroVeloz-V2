using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.TransitionPolices.Polices.SubscriptionPolicy
{
    public interface ISubscriptionPolicys
    {
        //Desconectar el canal de subscripcion cuando se cree el enum correspondiente
        
        //metodo para manejas las politicas de subscripcion
        public bool CanSubscribe(Guid flight, /*SubscriptionChannel channel,*/ string contactValue); 

    }
}
