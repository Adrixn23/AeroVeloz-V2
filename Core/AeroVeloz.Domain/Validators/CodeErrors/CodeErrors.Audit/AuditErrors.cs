using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.Audits
{
    public static class AuditErrors
    {
        public static DomainError InvalidAuditType =>
            DomainError.Create("AUDIT_01", "El tipo de auditoría especificado no es válido");

        public static DomainError InvalidUserId =>
            DomainError.Create("AUDIT_02", "El ID de usuario debe ser un GUID válido");

        public static DomainError EntityNameRequired =>
            DomainError.Create("AUDIT_03", "El nombre de la entidad es obligatorio para el registro de auditoría");

        public static DomainError InvalidDateRange =>
            DomainError.Create("AUDIT_04", "El rango de fechas para consulta de auditoría no es válido");

        public static DomainError AuditEntryImmutable =>
            DomainError.Create("AUDIT_05", "Los registros de auditoría son inmutables y no pueden ser modificados");

        public static DomainError MaxEntityNameLength =>
            DomainError.Create("AUDIT_06", "El nombre de la entidad no puede exceder los 30 caracteres");

        public static DomainError RetentionPolicyViolation =>
            DomainError.Create("AUDIT_07", "La consulta viola las políticas de retención de datos de auditoría");
    }
}
