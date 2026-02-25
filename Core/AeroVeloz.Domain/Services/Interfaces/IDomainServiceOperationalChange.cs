using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Operations;
using System.ComponentModel.DataAnnotations;

namespace AeroVeloz.Domain.Services.Interfaces
{
    public interface IDomainServiceOperationalChange
    {
        ValidationResult validation(Flight flight, OperationChange operation , OperationalChangeType operationalChange);
        OperationChange create();
    }
}
