using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Models.Operational;
using AeroVeloz.Application.DTOs.Operations;

namespace AeroVeloz.Application.Repositories.Operational
{
    public  interface IOperationalRepository : IBRepository<Domain.Entities.Operations.OperationChange>
    {

        Task<OperationalModel> GetByOperationAsync(Guid operationId);
        Task<IReadOnlyCollection<OperationalModel>> GetFlightChangesAsync(short flightNumber);
        Task<IReadOnlyCollection<OperationalDetailModel>> GetAirportChangesAsync(int orgId);
        Task<IReadOnlyCollection<FlightOperationDto>> GetFlightOperationsAsync(short flightNumber);
        Task<string?> GetOperationalTypeNameAsync(short typeId);

    }
}
