using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Audits;
using AeroVeloz.Domain.Models.Audit;

namespace AeroVeloz.Application.Repositories.Audit
{

    public interface IAuditRepository
    {

        Task<bool> CreateAsync(Domain.Entities.Audits.Audit audit);

        Task<IReadOnlyCollection<AuditDetailModel>> GetByOrganizationAsync(int orgId, DateTime? from = null, DateTime? to = null);

        Task<IReadOnlyCollection<AuditDetailModel>> GetByUserAsync(Guid userId, DateTime? from = null, DateTime? to = null);

        Task<bool> ExistsAsync(Guid auditId);

        Task<ValidationResult> ValidateAuditEntryAsync(Guid userId, short auditTypeId, string? entityName);
    }
}
