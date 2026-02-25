using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.SuperAdmin
{
    public static class SuperAdminErrors
    {
        public static DomainError SuperAdminAccessRequired =>
            DomainError.Create("SUPER_01", "Se requieren privilegios de super administrador para esta operación");

        public static DomainError InvalidUserManagementOperation =>
            DomainError.Create("SUPER_02", "La operación de gestión de usuarios no es válida");

        public static DomainError CannotDeactivateSelf =>
            DomainError.Create("SUPER_03", "No puede desactivar su propia cuenta");

        public static DomainError CannotModifyOtherSuperAdmin =>
            DomainError.Create("SUPER_04", "No puede modificar otros usuarios super administrador");

        public static DomainError RoleAssignmentFailed =>
            DomainError.Create("SUPER_05", "Error al asignar el rol al usuario");

        public static DomainError OrganizationNotFound =>
            DomainError.Create("SUPER_06", "La organización especificada no existe");

        public static DomainError BackupCreationFailed =>
            DomainError.Create("SUPER_07", "Error al crear el respaldo del sistema");

        public static DomainError MaintenanceWindowRequired =>
            DomainError.Create("SUPER_08", "La operación requiere una ventana de mantenimiento");

        public static DomainError InvalidSystemOperation =>
            DomainError.Create("SUPER_09", "La operación del sistema especificada no es válida");

        public static DomainError AirportHasActiveFlights =>
            DomainError.Create("SUPER_10", "No se puede desactivar el aeropuerto mientras tenga vuelos activos");
    }
}
