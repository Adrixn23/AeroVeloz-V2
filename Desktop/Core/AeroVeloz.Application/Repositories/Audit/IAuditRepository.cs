using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Models.Audit;

namespace AeroVeloz.Application.Repositories.Audit
{

    public interface IAuditRepository
    {

        Task<bool> CreateAsync(Domain.Entities.Audit.Audit audit);

        Task<IReadOnlyCollection<AuditDetailModel>> GetByOrganizationAsync(int orgId);

        Task<IReadOnlyCollection<AuditDetailModel>> GetByUserAsync(Guid userId);

        //Task<bool> ExistsAsync(Guid auditId); ->metodo comentado ya que no se encuentra en uso de momento por el cambio de enfoque que se realizo, favor descomentar para futuros puntos de mejora    

        Task<ValidationResult> ValidateAuditEntryAsync(Guid userId, short auditTypeId, string? entityName);
    }
}