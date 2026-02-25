using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.User.securtiy
{
    public static class AuthorizationErrors
    {
        public static DomainError InsufficientPermissions =>
            DomainError.Create("AUTHZ_01", "No tiene permisos suficientes para realizar esta acción");

        public static DomainError AirportAccessDenied =>
            DomainError.Create("AUTHZ_02", "No tiene autorización para acceder a este aeropuerto");

        public static DomainError FlightAccessDenied =>
            DomainError.Create("AUTHZ_03", "No tiene autorización para acceder a este vuelo");

        public static DomainError RoleNotFound =>
            DomainError.Create("AUTHZ_04", "El rol especificado no existe");

        public static DomainError OrganizationAccessDenied =>
            DomainError.Create("AUTHZ_05", "No pertenece a la organización requerida");

        public static DomainError AdminAccessRequired =>
            DomainError.Create("AUTHZ_06", "Se requieren privilegios de administrador para esta operación");

        public static DomainError SuperAdminAccessRequired =>
            DomainError.Create("AUTHZ_07", "Se requieren privilegios de super administrador para esta operación");
    }
}
