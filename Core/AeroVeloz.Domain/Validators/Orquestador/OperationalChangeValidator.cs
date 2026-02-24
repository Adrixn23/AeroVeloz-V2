using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.Validators.interfaces;
using AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.Operations;
using AeroVeloz.Domain.TransitionPolices;

namespace AeroVeloz.Domain.Validators.Orquestador
{
    public class OperationalChangeValidator : IOperationalChangeValidator
    {
        private readonly IChangeTypePolicy _changeTypePolicy;

     
        private readonly IFlightLifeCiclyePolicy _flightLifeCiclyePolicy;
        public OperationalChangeValidator(IChangeTypePolicy changeTypePolicy, IFlightLifeCiclyePolicy flightLifeCiclyePolicy) {
            _changeTypePolicy = changeTypePolicy;
            _flightLifeCiclyePolicy = flightLifeCiclyePolicy;
        }

        public void ValidateManualChange(OperationChange operation)
        {
            //agregar logica de validation de elementos para la visbilidad automatica de vuelos <= 48 horas
            //ademas de gestionar elementos de state para bloque al equipo operacional si el vuelo esta en vuelo
        }

        public ValidationResult ValidateOperational(OperationChange operation)
        {
            var errors = new List<DomainError>();

            if (string.IsNullOrEmpty(operation.codeAirline) || operation.codeAirline.Length < 3)
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

            /*if(operation.previousValue == FlightState.EnVuelo && 
             * operation.operationalChangeType == OperationalChangeType.Cancelled)
             * errors.Add(OperationalChangeErrors.InvalidChangeOperational);
             
             */

            var result = new ValidationResult();
            return result.Failur(errors); 
        }
    }
}
