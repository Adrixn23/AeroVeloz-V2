using AeroVeloz.Domain.Common.CodeErrors;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Operations;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Services.Interfaces.Operational;
using AeroVeloz.Domain.Validators.interfaces.Operations;

namespace AeroVeloz.Domain.Validators.Orquestador.Operational
{
    /// <summary>
    /// Implementación del validador de cambios operacionales que orquesta las reglas
    /// de negocio para la creación de operaciones sobre vuelos.
    /// Verifica la existencia previa de la operación, la duplicidad del tipo operacional
    /// y la validez del vuelo asociado (estado y fecha).
    /// </summary>
    public class OperationalChangeValidator : IOperationalChangeValidator
    {
        private readonly IDomainServiceOperationalChange _domainServiceOperationalChange;

       
        public OperationalChangeValidator(IDomainServiceOperationalChange domainServiceOperationalChange)
        {
            _domainServiceOperationalChange = domainServiceOperationalChange;

        }


        public async Task<ValidationResult> ValidateForCreateOperational(OperationChange operation)
        {
            var errors = new List<ErrosValidationResults>();
            if (operation == null)
            {
                errors.Add(OperationalChangeErrors.InvalidOperation);
                return new ValidationResult().Failur(errors);
            }

            // Verificar si ya existe una operación con el mismo ID
            if (await _domainServiceOperationalChange.OperationExistsAsync(operation.Id))
                errors.Add(OperationalChangeErrors.OperationExist);

            // Verificar si ya existe una operación del mismo tipo para el mismo vuelo
            if (await _domainServiceOperationalChange.OperationAlreadyRegisteredAsync(operation.Id, operation.idOperationalType))
                errors.Add(OperationalChangeErrors.OperationExistType);

            // Validar que el vuelo asociado sea válido (existe, no está cancelado, y tiene fecha vigente)
            if(await _domainServiceOperationalChange.OperationConsultFlightValid(operation.flightNumber))
            {
                errors.Add(OperationalChangeErrors.OperationInvalidFlight);
                errors.Add(OperationalChangeErrors.OperationInvalidFlightCancelled);
                errors.Add(OperationalChangeErrors.OperationInvalidFlightPast);
            }

            if (errors.Any())
            {
                return new ValidationResult().Failur(errors);
            }



           return new ValidationResult().Success();
        }
    }
}
