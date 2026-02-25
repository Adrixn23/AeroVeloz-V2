using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.User.securtiy

{
    public static class AuthenticationErrors
    {
        public static DomainError InvalidCredentials =>
            DomainError.Create("AUTH_01", "Las credenciales proporcionadas no son válidas");

        public static DomainError UserNotFound =>
            DomainError.Create("AUTH_02", "El usuario especificado no existe en el sistema");

        public static DomainError UserLocked =>
            DomainError.Create("AUTH_03", "La cuenta de usuario está bloqueada temporalmente por múltiples intentos fallidos");

        public static DomainError UserInactive =>
            DomainError.Create("AUTH_04", "La cuenta de usuario está inactiva");

        public static DomainError WeakPassword =>
            DomainError.Create("AUTH_05", "La contraseña no cumple con los requisitos mínimos de seguridad");

        public static DomainError UsernameRequired =>
            DomainError.Create("AUTH_06", "El nombre de usuario es obligatorio");

        public static DomainError PasswordRequired =>
            DomainError.Create("AUTH_07", "La contraseña es obligatoria");

        public static DomainError SessionExpired =>
            DomainError.Create("AUTH_08", "La sesión ha expirado, debe autenticarse nuevamente");
    }

   
}
