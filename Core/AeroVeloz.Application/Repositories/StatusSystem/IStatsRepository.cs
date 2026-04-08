using AeroVeloz.Application.DTOs.StatusSystem;

namespace AeroVeloz.Application.Repositories.StatusSystem
{
    public interface IStatsRepository
    {
        Task<GlobalStatsDto> GetGlobalStatsAsync(int orgId);
    }
}