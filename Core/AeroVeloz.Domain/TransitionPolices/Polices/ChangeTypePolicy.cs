using AeroVeloz.Domain.TransitionPolices;
using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.Polices
{
    public  class ChangeTypePolicy : IChangeTypePolicy
    {

        //agregar campo/argumento cuando se cree lo descripto en la interface
        public bool IsAllowed()
        {
            throw new NotImplementedException(); // agregar aqui logica de negocio
        }
    }
}
