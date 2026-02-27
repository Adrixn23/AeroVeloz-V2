using AeroVeloz.Domain.TransitionPolices;
using System;
using System.Collections.Generic;
using System.Text;

namespace AeroVeloz.Domain.Visibility
{
    public class VisibilityPolicy : IVisibilityPolicy
    {
        public Dictionary<string, HashSet<string>> AllowedFieldsByRoles { get; private set; }

        public VisibilityPolicy() {

            AllowedFieldsByRoles = new Dictionary<string, HashSet<string>>();


        }

        public bool CanSeeField(string role, string fieldName)
        {
            throw new NotImplementedException(); //agregar logica
        }

       //agregar metodo extra cuando se aplique lo descripto en la interfaz
    }
}
