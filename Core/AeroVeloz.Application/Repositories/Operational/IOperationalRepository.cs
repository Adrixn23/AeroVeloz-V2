using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Models.Operational;

namespace AeroVeloz.Application.Repositories.Operational
{
    public  interface IOperationalRepository : IBRepository<Domain.Entities.Operations.OperationChange, Guid>
    {
        Task<OperationalModel?> GetByIdAsync(Guid id);
        Task<OperationalModel> GetByOperationAsync(Guid operationId);
        Task<IReadOnlyCollection<OperationalModel>> GetFlightChangesAsync(short flightNumber);
        Task<IReadOnlyCollection<OperationalModel>> GetAirportChangesAsync(int orgId);
      
    }
}
