using AeroVeloz.Application.Contracts.Audit;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Application.Repositories.Auth;
using AeroVeloz.Domain.Models.Audit;

namespace AeroVeloz.Application.Handlers.Audit
{
    public class AuditService : IAuditServicie
    {
        private readonly IAuditRepository _repo;
        private readonly IUserRepositoryAuthorization _auth;

        public AuditService(IAuditRepository repo, IUserRepositoryAuthorization auth)
        {
            _repo = repo;
            _auth = auth;
        }

        public async Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByOrganizationAsync(
            int orgId, DateTime? from, DateTime? to, Guid userId)
        {
            var authResult = await _auth.CanViewAuditLogsAsync(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<IReadOnlyCollection<AuditDetailModel>>.FromValidation(authResult);

            var audits = await _repo.GetByOrganizationAsync(orgId, from, to);
            return OperationResult<IReadOnlyCollection<AuditDetailModel>>.Ok(audits);
        }

        public async Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByUserAsync(
            Guid targetUserId, DateTime? from, DateTime? to, Guid userId, int orgId)
        {
            var authResult = await _auth.CanViewAuditLogsAsync(userId, orgId);
            if (!authResult.IsValid)
                return OperationResult<IReadOnlyCollection<AuditDetailModel>>.FromValidation(authResult);

            var audits = await _repo.GetByUserAsync(targetUserId, from, to);
            return OperationResult<IReadOnlyCollection<AuditDetailModel>>.Ok(audits);
        }
    }
}
