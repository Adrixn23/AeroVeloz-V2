using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Audit
{
    /// <summary>
    /// Clase estática que centraliza todos los errores de validación relacionados con la auditoría.
    /// Incluye errores de tipo de auditoría inválido, datos obligatorios faltantes,
    /// integridad de registros y políticas de retención de datos.
    /// </summary>
    public static class AuditErrors
    {
        public static ErrosValidationResults InvalidAuditType =>
            ErrosValidationResults.Create("AUDIT_01", "El tipo de auditoría especificado no es válido");

        public static ErrosValidationResults InvalidUserId =>
            ErrosValidationResults.Create("AUDIT_02", "El ID de usuario debe ser un GUID válido");

        public static ErrosValidationResults EntityNameRequired =>
            ErrosValidationResults.Create("AUDIT_03", "El nombre de la entidad es obligatorio para el registro de auditoría");

        public static ErrosValidationResults InvalidDateRange =>
            ErrosValidationResults.Create("AUDIT_04", "El rango de fechas para consulta de auditoría no es válido");

        public static ErrosValidationResults AuditEntryImmutable =>
            ErrosValidationResults.Create("AUDIT_05", "Los registros de auditoría son inmutables y no pueden ser modificados");

        public static ErrosValidationResults MaxEntityNameLength =>
            ErrosValidationResults.Create("AUDIT_06", "El nombre de la entidad no puede exceder los 30 caracteres");

        public static ErrosValidationResults RetentionPolicyViolation =>
            ErrosValidationResults.Create("AUDIT_07", "La consulta viola las políticas de retención de datos de auditoría");
    }
}
