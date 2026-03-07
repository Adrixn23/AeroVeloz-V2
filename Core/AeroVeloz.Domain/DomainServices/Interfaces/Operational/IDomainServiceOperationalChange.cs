using AeroVeloz.Domain.Common.Enums.Organization;
using AeroVeloz.Domain.Entities.Flights;

namespace AeroVeloz.Domain.Services.Interfaces.Operational
{
    public interface IDomainServiceOperationalChange
    {

        Task<bool> OperationExistsAsync(Guid operationId);

        Task<bool> FlightExistsAsync(short flightNumber);

  
        Task<bool> OperationAlreadyRegisteredAsync(Guid operationId, OperationalChangeType type );

        Task<bool> OperationBelongsToOrganizationAsync(Guid operationId, int organizationId);

    }
}
