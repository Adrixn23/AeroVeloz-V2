using AeroVeloz.Domain.TransitionPolices;
using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.Subscriptions
{
    public class SubscriptionPolicy : ISubscriptionPolicys
    {
        public bool CanSubscribe(Guid flight, string contactValue)
        {
            throw new NotImplementedException(); // crear aqui comportamiento de la susbcripciones -> logica de negocio
        }

        //agregar el otro campo cuando se cumpla la condicion descripta en la interface
    }
}
