using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors
{
    public static class SystemErrors
    {
        public static ErrosValidationResults DatabaseFailure =>
            ErrosValidationResults.Create("SYS_DB_01", "Ocurrió un error al procesar los datos en la base de datos.");

        public static ErrosValidationResults ServiceUnavailable =>
            ErrosValidationResults.Create("SYS_NET_02", "El servicio no se encuentra disponible momentáneamente. Por favor, inténtelo de nuevo más tarde.");

        public static ErrosValidationResults EntityNotFound =>
            ErrosValidationResults.Create("SYS_NF_03", "La entidad solicitada no fue encontrada en el sistema.");
            
        public static ErrosValidationResults UnexpectedError =>
            ErrosValidationResults.Create("SYS_ERR_00", "Ocurrió un error inesperado al procesar su solicitud.");
    }
}
