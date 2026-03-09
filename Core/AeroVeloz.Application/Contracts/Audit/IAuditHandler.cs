using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Domain.Models.Audit;

namespace AeroVeloz.Application.Contracts.Audit
{
    public interface IAuditHandler
    {
        Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByOrganizationAsync(int orgId, DateTime? from, DateTime? to, Guid userId);
        Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByUserAsync(Guid targetUserId, DateTime? from, DateTime? to, Guid userId, int orgId);
    }
}
