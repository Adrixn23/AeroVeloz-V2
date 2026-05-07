using System;
using System.Threading.Tasks;
using AeroVeloz.Application.Contracts.StatusSystem;
using AeroVeloz.Application.DTOs.StatusSystem;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Application.Repositories.StatusSystem;
using AeroVeloz.Transversal.Contracts.Monitoring;

namespace AeroVeloz.Application.Services.StatusSystem
{
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _statsRepository;
        private readonly IUserRepositoryAuthorization _auth;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;

        public StatsService(
            IStatsRepository statsRepository, 
            IUserRepositoryAuthorization auth,
            IOrganizationMonitoringLogger monitoringLogger)
        {
            _statsRepository = statsRepository;
            _auth = auth;
            _monitoringLogger = monitoringLogger;
        }

        public async Task<OperationResult<GlobalStatsDto>> GetGlobalStatsAsync(Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<GlobalStatsDto>.FromValidation(authResult);

                var stats = await _statsRepository.GetGlobalStatsAsync(orgId);
                return OperationResult<GlobalStatsDto>.Ok(stats);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new Transversal.Monitoring.MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "StatsService.GetGlobalStatsAsync",
                    Message = "Error inesperado al obtener las estadísticas"
                }, ex);
                return OperationResult<GlobalStatsDto>.Fail("STATS_ERROR", "Error inesperado al obtener estadísticas globales");
            }
        }

        public async Task<OperationResult<AirportAdminStatsDto>> GetAirportStatsAsync(Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.AuthorizeOrganizationAccessAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<AirportAdminStatsDto>.FromValidation(authResult);

                var stats = await _statsRepository.GetAirportStatsAsync(orgId);
                return OperationResult<AirportAdminStatsDto>.Ok(stats);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new Transversal.Monitoring.MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "StatsService.GetAirportStatsAsync",
                    Message = "Error inesperado al obtener las estadísticas del aeropuerto"
                }, ex);
                return OperationResult<AirportAdminStatsDto>.Fail("AIRPORT_STATS_ERROR", "Error inesperado al obtener estadísticas del aeropuerto");
            }
        }
    }
}
