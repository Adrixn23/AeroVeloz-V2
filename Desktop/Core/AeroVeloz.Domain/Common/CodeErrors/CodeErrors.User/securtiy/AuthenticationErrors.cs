using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User.securtiy

{


    public static class AuthenticationErrors
    {
        public static ErrosValidationResults InvalidCredentials =>
            ErrosValidationResults.Create("AUTH_01", "Las credenciales proporcionadas no son válidas");

        public static ErrosValidationResults UserNotFound =>
            ErrosValidationResults.Create("AUTH_02", "El usuario especificado no existe en el sistema o organización en la que ha intentado loguearse");

        public static ErrosValidationResults UserLocked =>
            ErrosValidationResults.Create("AUTH_03", "La cuenta de usuario está bloqueada temporalmente por múltiples intentos fallidos");

        public static ErrosValidationResults UserInactive =>
            ErrosValidationResults.Create("AUTH_04", "La cuenta de usuario está inactiva");

        public static ErrosValidationResults NoExistOrgByUsers =>
            ErrosValidationResults.Create("AUTH_05", "La organización a la cual esta intentando acceder no existe o se encuentra desactivada");

        public static ErrosValidationResults DesktopAccessDenied =>
            ErrosValidationResults.Create("AUTH_06", "Este usuario no tiene acceso al portal de escritorio. Solo usuarios AIRPORTADMIN, SYSTEMADMIN y OPERATIONAIRPORT pueden acceder");

        public static ErrosValidationResults AccountLockedByAttempts =>
            ErrosValidationResults.Create("AUTH_07", "La cuenta ha sido bloqueada por 15 minutos debido a múltiples intentos fallidos de inicio de sesión");

    }

   
}
