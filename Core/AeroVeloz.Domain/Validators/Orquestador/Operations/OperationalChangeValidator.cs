using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Validators.interfaces.Operations;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Operations;

namespace AeroVeloz.Domain.Validators.Orquestador.Operations
{
    public class OperationalChangeValidator 
    {
<<<<<<< HEAD
        private readonly IChangeTypePolicy _changeTypePolicy;

        public OperationalChangeValidator(IChangeTypePolicy changeTypePolicy) {
            _changeTypePolicy = changeTypePolicy;
        
        }
        public ValidationResult ValidateOperational(OperationChange operation)
        {
            var errors = new List<DomainError>();

            if (string.IsNullOrEmpty(operation.codeAirline) || operation.codeAirline.Length < 4)
                errors.Add(OperationalChangeErrors.InvalidAirlineCode);
            if (operation.flightNumber <= 0)
                errors.Add(OperationalChangeErrors.InvalidFlightNumber);
            if (operation == null)
                errors.Add(OperationalChangeErrors.InvalidChangeOperational);
            if (!_changeTypePolicy.IsAllowed(operation!.operationalChangeType))
                errors.Add(OperationalChangeErrors.InvalidChangeOperational);
            if (operation.changeAt < DateTime.UtcNow)
                errors.Add(OperationalChangeErrors.InvalidChangeOperationDateInvalidPast);

            //descomentar esto cuando se agregue el modulo de vuelos

            /*if(operation.previousValue == FlightState.EnVuelo || 
             * operation.operationalChangeType == OperationalChangeType.Cancelled)
             * errors.Add(OperationalChangeErrors.InvalidChangeOperational);
             
             */

            var result = new ValidationResult();
            return  errors.Any() ?  result.Failur(errors) : result.Success(); 
        }
=======
        
>>>>>>> modulo-aeropuertuario
    }
}
