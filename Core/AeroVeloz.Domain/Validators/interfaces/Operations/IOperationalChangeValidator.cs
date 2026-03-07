using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Validators.interfaces.Operations
{
    public interface IOperationalChangeValidator
    {
        ValidationResult ValidateOperational(OperationChange operation);
    }
}
