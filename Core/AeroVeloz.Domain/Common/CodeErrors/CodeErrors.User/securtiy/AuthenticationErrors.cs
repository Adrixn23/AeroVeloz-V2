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

        public static ErrosValidationResults WeakPassword =>
            ErrosValidationResults.Create("AUTH_05", "La contraseña no cumple con los requisitos mínimos de seguridad");

        public static ErrosValidationResults UsernameRequired =>
            ErrosValidationResults.Create("AUTH_06", "El nombre de usuario es obligatorio");

        public static ErrosValidationResults PasswordRequired =>
            ErrosValidationResults.Create("AUTH_07", "La contraseña es obligatoria");

        public static ErrosValidationResults SessionExpired =>
            ErrosValidationResults.Create("AUTH_08", "La sesión ha expirado, debe autenticarse nuevamente");

        public static ErrosValidationResults NoExistOrgByUsers =>
            ErrosValidationResults.Create("AUTH_09", "La organización a la cual esta intentando acceder no existe o se encuentra desactivada");

    }

   
}
