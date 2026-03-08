using AeroVeloz.Domain.Entities.Operations;

namespace AeroVeloz.Domain.Services.Interfaces.Operational
{
    public interface IDomainServiceOperationalChange
    {

        Task<bool> OperationExistsAsync(Guid operationId);

        Task<bool> OperationAlreadyRegisteredAsync(Guid operationId, short typeOp);

        Task<bool> OperationBelongsToOrganizationAsync(Guid operationId, int organizationId);

        Task<bool> OperationConsultFlightValid(short flightNumber);

    }
}
