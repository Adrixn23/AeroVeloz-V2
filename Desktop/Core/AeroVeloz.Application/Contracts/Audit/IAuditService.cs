using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Domain.Models.Audit;

namespace AeroVeloz.Application.Contracts.Audit
{
    public interface IAuditService
    {
        Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByOrganizationAsync(int orgId, Guid userId);
        Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByUserAsync(Guid targetUserId,  Guid userId, int orgId);
    }
}
