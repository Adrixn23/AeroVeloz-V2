using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.ValidationBase
{
    class FlightDomainException : DomainExceptions
    {
        public ValidationResult? ValidationResult { get; }
   
           // El constructor que recibe el mensaje y los errores
           public FlightDomainException(string message, ValidationResult validationResult)
               : base(message) // Le pasamos el mensaje a domainexception
            {
                ValidationResult = validationResult;
            }
}
}
