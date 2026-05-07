using AeroVeloz.Application.Services.Result;
using AeroVeloz.Domain.Models.Audit;


namespace AeroVeloz.Application.Contracts.Audit
{
    public interface IAuditService
    {
        Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByOrganizationAsync(int orgId, DateTime? from, DateTime? to, Guid userId);
        Task<OperationResult<IReadOnlyCollection<AuditDetailModel>>> GetByUserAsync(Guid targetUserId, DateTime? from, DateTime? to, Guid userId, int orgId);
    }
}