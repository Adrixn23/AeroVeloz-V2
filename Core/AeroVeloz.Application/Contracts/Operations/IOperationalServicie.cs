using AeroVeloz.Application.DTOs.Operations;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Domain.Models.Operational;

namespace AeroVeloz.Application.Contracts.Operations
{
    public interface IOperationalServicie
    {
        Task<OperationResult<bool>> RegisterAsync(OperationalChangeSaveDto dto, Guid userId, int orgId);
        Task<OperationResult<OperationalModel>> GetByIdAsync(Guid operationId, Guid userId, int orgId);
        Task<OperationResult<IReadOnlyCollection<OperationalModel>>> GetFlightChangesAsync(short flightNumber, Guid userId, int orgId);
        Task<OperationResult<IReadOnlyCollection<OperationalDetailModel>>> GetAirportChangesAsync(Guid userId, int orgId);

    }
}
