using AeroVeloz.Application.Contracts.Audit;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Models.Audit;
using AeroVeloz.Transversal.Contracts.Monitoring;
using AeroVeloz.Transversal.Monitoring;

namespace AeroVeloz.Application.Handlers.Audit
{
    public class AuditService : IAuditServicie
    {
        private readonly IAuditRepository _repo;
        private readonly IUserRepositoryAuthorization _auth;
        private readonly IOrganizationMonitoringLogger _monitoringLogger;

        public AuditService(IAuditRepository repo, IUserRepositoryAuthorization auth, IOrganizationMonitoringLogger monitoringLogger)
        {
            _repo = repo;
            _auth = auth;
            _monitoringLogger = monitoringLogger;
        }


        public async Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByOrganizationAsync(
            int orgId, Guid userId)
        {
            try
            {
                var authResult = await _auth.CanViewAuditLogsAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<IReadOnlyCollection<AuditDetailModel>>.FromValidation(authResult);

                var audits = await _repo.GetByOrganizationAsync(orgId);
                return OperationResult<IReadOnlyCollection<AuditDetailModel>>.Ok(audits);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AuditService.GetByOrganizationAsync",
                    Message = "Error inesperado al obtener registros de auditoría por organización"
                }, ex);
                return OperationResult<IReadOnlyCollection<AuditDetailModel>>.Fail("AUDIT_ERROR", "Error inesperado al obtener auditoría");
            }
        }

        public async Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByUserAsync(
            Guid targetUserId, Guid userId, int orgId)
        {
            try
            {
                var authResult = await _auth.CanViewAuditLogsAsync(userId, orgId);
                if (!authResult.IsValid)
                    return OperationResult<IReadOnlyCollection<AuditDetailModel>>.FromValidation(authResult);

                var audits = await _repo.GetByUserAsync(targetUserId);
                return OperationResult<IReadOnlyCollection<AuditDetailModel>>.Ok(audits);
            }
            catch (Exception ex)
            {
                await _monitoringLogger.LogSystemFaultAsync(new MonitoringLogEntry
                {
                    OrganizationId = orgId,
                    UserId = userId,
                    Source = "AuditService.GetByUserAsync",
                    Message = $"Error inesperado al obtener registros de auditoría del usuario: {targetUserId}"
                }, ex);
                return OperationResult<IReadOnlyCollection<AuditDetailModel>>.Fail("AUDIT_ERROR", "Error inesperado al obtener auditoría del usuario");
            }
        }

    }
}
