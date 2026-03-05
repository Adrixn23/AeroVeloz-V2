using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.interfaces.Operations
{
    public interface IOperationalChangeValidator
    {
        ValidationResult ValidateOperational(OperationChange operation);

        public void ValidateManualChange(OperationChange operation /*, Flight currentFlight*/);
    }
}
