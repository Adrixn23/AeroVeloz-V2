using AeroVeloz.Application.DTOs.Operations;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Domain.Models.Operational;

namespace AeroVeloz.Application.Contracts.Operations
{
    public interface IOperationalService
    {
        Task<OperationResult<bool>> RegisterAsync(OperationalChangeSaveDto dto, Guid userId, int orgId);
        Task<OperationResult<bool>> UpdateAsync(OperationalChangeUpdateDto dto, Guid userId, int orgId);
        Task<OperationResult<OperationalModel>> GetByIdAsync(Guid operationId, Guid userId, int orgId);
        Task<OperationResult<IReadOnlyCollection<OperationalModel>>> GetFlightChangesAsync(short flightNumber, Guid userId, int orgId);
        Task<OperationResult<IReadOnlyCollection<OperationalDetailModel>>> GetAirportChangesAsync(Guid userId, int orgId);
        Task<OperationResult<bool>> DesactiveOperational(OperationalChangeRemoveDto dto, Guid userId, int orgId);
    }
}
