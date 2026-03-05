using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.User
{
    public static class UserErrors
    {
        public static ErrosValidationResults OrganizationNotFound =>
            ErrosValidationResults.Create("USER_01", "La organización especificada no existe o no se encuentra activa.");

        public static ErrosValidationResults UserAssociateWithOrganization =>
            ErrosValidationResults.Create("USER_02", "El usuario debe estar vinculado a una organización existente válida.");

        public static ErrosValidationResults UserInvalid =>
            ErrosValidationResults.Create("USER_03", "El usuario que ha intentado crear no cumple con los parámetros mínimos necesarios para existir.");

        public static ErrosValidationResults UserIsExist =>
            ErrosValidationResults.Create("USER_04", "Este usuario ya existe dentro de esta organización por favor revise o consulte sus usuarios activos e inactivos");

        public static ErrosValidationResults UserExistInOrganization =>
            ErrosValidationResults.Create("USER_05", "Este usuario ya existe en esta organización, por favor verifique su listado de usuarios activos o inactivos para consultar sobre el mismo");
    }
}