using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User.securtiy
{
    public static class AuthorizationErrors
    {
        public static ErrosValidationResults InsufficientPermissions =>
            ErrosValidationResults.Create("AUTHZ_01", "No tiene permisos suficientes para realizar esta acción");

        public static ErrosValidationResults AirportAccessDenied =>
            ErrosValidationResults.Create("AUTHZ_02", "No tiene autorización para acceder a este aeropuerto");

        public static ErrosValidationResults FlightAccessDenied =>
            ErrosValidationResults.Create("AUTHZ_03", "No tiene autorización para acceder a este vuelo");

        public static ErrosValidationResults RoleNotFound =>
            ErrosValidationResults.Create("AUTHZ_04", "El rol especificado no existe");

        public static ErrosValidationResults OrganizationAccessDenied =>
            ErrosValidationResults.Create("AUTHZ_05", "No pertenece a la organización requerida");

        public static ErrosValidationResults AdminAccessRequired =>
            ErrosValidationResults.Create("AUTHZ_06", "Se requieren privilegios de administrador para esta operación");

        public static ErrosValidationResults SuperAdminAccessRequired =>
            ErrosValidationResults.Create("AUTHZ_07", "Se requieren privilegios de super administrador para esta operación");
    }
}
