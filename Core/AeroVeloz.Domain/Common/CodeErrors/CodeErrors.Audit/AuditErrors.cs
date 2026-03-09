using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Audit
{
    public static class AuditErrors
    {       
        public static ErrosValidationResults InvalidUserId =>
            ErrosValidationResults.Create("AUDIT_01", "El ID de usuario debe ser un GUID válido y existente en el sistema");

        public static ErrosValidationResults EntityNameRequired =>
            ErrosValidationResults.Create("AUDIT_02", "El nombre de la entidad es obligatorio para el registro de auditoría");

        public static ErrosValidationResults MaxEntityNameLength =>
            ErrosValidationResults.Create("AUDIT_03", "El nombre de la entidad no puede exceder los 30 caracteres");

        public static ErrosValidationResults AuditTypeNotFound =>
            ErrosValidationResults.Create("AUDIT_04", "El tipo de auditoría no existe en el catálogo del sistema");

        public static ErrosValidationResults UserNotFoundForAudit =>
            ErrosValidationResults.Create("AUDIT_05", "El usuario asociado a la acción auditada no existe en el sistema");


    

        
    }
}
