using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.TransitionPolices;

namespace AeroVeloz.Domain.Polices
{
    public  class ChangeTypePolicy : IChangeTypePolicy
    {
        public HashSet<OperationalChangeType> AllowedTypes = new HashSet<OperationalChangeType>(); // set de operaciones permitidas 
        public bool IsAllowed(OperationalChangeType type)
        {
            return AllowedTypes.Contains(type); // este metodo lo que uno que hace es decirnos si la operacion hacer realizada es valida
                                                //lo usaremos entonces para los elementos del ciclo de vida del vuelo para verificar el cambio operational en x momento                              
                                                //empleando entoncesel desarrollo de la estructura que nos permite entonces validar si se puede efectuar un cambio como tal
        }
    }
}
