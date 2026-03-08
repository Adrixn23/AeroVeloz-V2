using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Entities.Operations;

namespace AeroVeloz.Domain.Services.Interfaces.Operational
{
    public interface IDomainServiceOperationalChange
    {

        Task<bool> OperationExistsAsync(Guid operationId);

        Task<bool> OperationAlreadyRegisteredAsync(Guid operationId, OperationalChangeType type );

        Task<bool> OperationBelongsToOrganizationAsync(Guid operationId, int organizationId);

    }
}
