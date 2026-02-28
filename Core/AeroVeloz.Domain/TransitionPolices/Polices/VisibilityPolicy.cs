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
            // Campos que el SAD define como publicss
            var publicFields = new HashSet<string> { "FlightNumber", "Origin", "Destination", "State", "Gate" };
            if (publicFields.Contains(fieldName)) return true;
            if (role == "Operaciones" || role == "Admin") return true;
            // si no entro en ninguno de los anteriores , se denegara el acceso
            return false;
        }



        //agregar metodo extra cuando se aplique lo descripto en la interfaz

        public bool IsVisibleToPublic(DateTime flightDate, DateTime now)
        {
            Double hoursDifference = Math.Abs((flightDate - now).TotalHours);
            return hoursDifference <= 48;
        }
    }
}