using AeroVeloz.Domain.Entities.Audits;
using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Validators.interfaces.Audit
{
    public interface IAuditValidator { 
        ValidationResult ValidateAuditEntry(Audits audits); 
        ValidationResult ValidateAuditQuery(string? entityName, DateTime? from, DateTime? to);
        ValidationResult ValidateUserAuditAccess(Guid requestingUserId, Guid targetUserId); }
}
