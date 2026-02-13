using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.TransitionPolices
{
    public interface IChangeTypePolicy
    {
        //Descomentar cuando se cree el enum correspondiente

        public bool IsAllowed(/*OperationalChangeTyoe type*/);
    }
}
