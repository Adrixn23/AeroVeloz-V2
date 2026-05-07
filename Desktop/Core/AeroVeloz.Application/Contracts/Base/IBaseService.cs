using AeroVeloz.Application.Handlers.Result;

namespace AeroVeloz.Application.Contracts.Base
{
    public interface IBaseService<TSaveDto, TUpdateDto, TEntityId>
    {
        Task<OperationResult<bool>> CreateAsync(TSaveDto dto, Guid userId, int orgId);
        Task<OperationResult<bool>> UpdateAsync(TUpdateDto dto, Guid userId, int orgId);
        Task<OperationResult<bool>> DeactivateAsync(TEntityId entityId, Guid userId, int orgId);
    }
}
