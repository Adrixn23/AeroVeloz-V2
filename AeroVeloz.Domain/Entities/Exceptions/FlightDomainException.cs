using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroVeloz.Domain.Flights;
namespace AeroVeloz.Domain.Entities.Exceptions
{
     public class FlightDomainException : Exception
    {

        public FlightDomainException(string message) : base(message) { 
        
        
        
        }
    }
}
