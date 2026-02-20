using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.TransitionPolices.Polices.Visibility
{
    public interface IVisibilityPolicy
    {

        //descomentar cuando se creen los dto de la capa de application

        public bool CanSeeField(string role, string fieldName);
        //public FlightDto ApplyVisibility (Flight flight, role  string);
    }
}
