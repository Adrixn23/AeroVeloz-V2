
using AeroVeloz.Domain.Entities.Audits;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Validators.interfaces.Audit;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Audit;

namespace AeroVeloz.Domain.Validators.Orquestador.Audits
{
    public class AuditValidator : IAuditValidator
    {
        public ValidationResult ValidateAuditEntry(Entities.Audits.Audits audits)
        {
            var errors = new List<ErrosValidationResults>();
            if (audits == null)
            { errors.Add(AuditErrors.InvalidAuditType); return new ValidationResult().Failur(errors); }
            if (audits.idUser == Guid.Empty) errors.Add(AuditErrors.InvalidUserId); 
            if (string.IsNullOrWhiteSpace(audits.nameEntity)) errors.Add(AuditErrors.EntityNameRequired);
            if (audits.nameEntity?.Length > 30) errors.Add(AuditErrors.MaxEntityNameLength); 
            if (audits.AuditType <= 0) errors.Add(AuditErrors.InvalidAuditType);
            var result = new ValidationResult(); 
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateAuditQuery(string? entityName, DateTime? from, DateTime? to)
        {
            var errors = new List<ErrosValidationResults>(); 
            if (from.HasValue && to.HasValue && from > to)
                errors.Add(AuditErrors.InvalidDateRange);
            if (from.HasValue && from > DateTime.UtcNow)
                errors.Add(AuditErrors.InvalidDateRange);               
            if (from.HasValue && from < DateTime.UtcNow.AddDays(-90))                
                errors.Add(AuditErrors.RetentionPolicyViolation);            
            var result = new ValidationResult();            
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public ValidationResult ValidateUserAuditAccess(Guid requestingUserId, Guid targetUserId)
        {
            var errors = new List<ErrosValidationResults>();
            if (requestingUserId == Guid.Empty || targetUserId == Guid.Empty) errors.Add(AuditErrors.InvalidUserId);
            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }
    }
}
