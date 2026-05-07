using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.codeError.CodeErrors.User.securtiy
{
    /// <summary>
    /// Clase estática que centraliza todos los errores de autorización del sistema.
    /// Contiene errores relacionados con permisos insuficientes, acceso denegado
    /// a recursos específicos y requisitos de roles administrativos.
    /// </summary>
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
        
        public static ErrosValidationResults OrganizationsNoValid =>
            ErrosValidationResults.Create("AUTHZ_08", "Esta organización no cuenta con los privilegios para " +
                "realizar esta acción, se ha intentado realizar una acción no correspondiente a este organismo  ");

        public static ErrosValidationResults OrganizationNoActive =>
            ErrosValidationResults.Create("AUTHZ_09", "Esta organización no se encuentra activa por lo que no puede realizar operaciones dentro del sistema");
        
    }
}
