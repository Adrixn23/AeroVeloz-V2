using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Services.Interfaces;
using AeroVeloz.Domain.TransitionPolices;
using System.ComponentModel.DataAnnotations;

namespace AeroVeloz.Domain.Services.orquestador
{
    public class DomainServiceOperationalChange : IDomainServiceOperationalChange
    {

        private readonly IChangeTypePolicy _changeTypePolicy;
        public DomainServiceOperationalChange(IChangeTypePolicy changeTypePolicy) { 
            _changeTypePolicy = changeTypePolicy;
        }

        public OperationChange create()
        {
            /*
             * return new OperationChange(//agregar parametros del operationChange aqui pero cuando se cree el elemento que consule el id user
             * 
             * );
             
             */
            return null;
        }

        public ValidationResult validation(Flight flight, OperationChange operation, OperationalChangeType operationalChange)
        {
           // var result = new ValidationResult();

            /*
             * if(flight.ScheduledDeparture)
             *   result.addError('FLIGHT_ALREADY_EXECUTED');
     
             */

            /*
              if(!_changeTypePolicy.IsAllowed(operationalChange))
                //result.addError('CHANGE_TYPE_REQUIERED');
            */
            return null;
        }
    }
}
