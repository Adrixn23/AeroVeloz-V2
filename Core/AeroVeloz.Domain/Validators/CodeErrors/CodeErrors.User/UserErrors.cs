using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.User
{
    public static class UserErrors
    {
        public static DomainError UserAdminAccessRequired =>
            DomainError.Create("USER_01", "Se requieren privilegios de usuario administrador para esta operación.");

        public static DomainError InvalidUserManagementOperation =>
            DomainError.Create("USER_02", "La operación de gestión de usuarios no es válida.");

        public static DomainError CannotDeactivateSelf =>
            DomainError.Create("USER_03", "No puede desactivar su propia cuenta.");

        public static DomainError CannotModifyOtherUserAdmin =>
            DomainError.Create("USER_04", "No puede modificar otros usuarios del tipo administrador.");

        public static DomainError CannotModifyOtherUserWithCurrentRole =>
            DomainError.Create("USER_05", "La acción que ha intentado realizar requiere un rol superior al que su perfil posee.");

        public static DomainError RoleAssignmentFailed =>
            DomainError.Create("USER_06", "Error al asignar el rol al usuario.");

        public static DomainError OrganizationNotFound =>
            DomainError.Create("USER_07", "La organización especificada no existe o no se encuentra activa.");

        public static DomainError UserAssociateWithOrganization =>
            DomainError.Create("USER_08", "El usuario debe estar vinculado a una organización existente válida.");

        public static DomainError AirportHasActiveFlights =>
            DomainError.Create("USER_09", "No se puede desactivar el aeropuerto mientras tenga vuelos activos.");

        public static DomainError UserInvalid =>
            DomainError.Create("USER_10", "El usuario que ha intentado crear no cumple con los parámetros mínimos necesarios para existir.");

        public static DomainError InvalidIdUser =>
            DomainError.Create("USER_11", "El Id que se ha intentado asignar al usuario no corresponde a un elemento válido. Por favor, verifique el valor proporcionado.");

        public static DomainError InvalidNameUser =>
            DomainError.Create("USER_12", "El nombre de usuario debe tener una longitud mínima de 6 caracteres, estar compuesto únicamente por letras y no contener dígitos ni caracteres especiales.");

        public static DomainError InvalidPasswordUser =>
            DomainError.Create("USER_13", "La contraseña ingresada no cumple con los estándares de creación. Debe contener una combinación de letras, números y caracteres especiales.");
    }
}