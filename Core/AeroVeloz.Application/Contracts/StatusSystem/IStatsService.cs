using AeroVeloz.Application.DTOs.StatusSystem;
using AeroVeloz.Application.Handlers.Result;

namespace AeroVeloz.Application.Contracts.StatusSystem
{
    public interface IStatsService
    {
        Task<OperationResult<GlobalStatsDto>> GetGlobalStatsAsync(Guid userId, int orgId);
    }
}