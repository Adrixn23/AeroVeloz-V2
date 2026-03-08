using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Operations
{
    /// <summary>
    /// Clase estática que centraliza todos los errores de validación relacionados con
    /// los cambios operacionales sobre vuelos. Incluye errores de organización inválida,
    /// operaciones duplicadas, vuelos no válidos y vuelos fuera de circulación.
    /// </summary>
    public static class OperationalChangeErrors
    {
        /// <summary>Error: La operación no es válida porque el organismo no fue encontrado o está desactivado.</summary>
        public static ErrosValidationResults InvalidaOperationByOrganization =>
             ErrosValidationResults.Create("OP_01", "La operación que ha intentando realizar no es valida para este organismo " +
                 "ya que el mismo no fue encontrado o se encuentra desactivado, favor confirmar el estado operativo de dicho organismo");

        /// <summary>Error: La operación no cumple con los requerimientos mínimos de creación.</summary>
        public static ErrosValidationResults InvalidOperation =>
            ErrosValidationResults.Create("OP_02", "La operación que se ha intentado crear no cumple con los requerimientos minimos para ser creada");

        /// <summary>Error: La operación ya existe para el vuelo y aerolínea indicados.</summary>
        public static ErrosValidationResults OperationExist =>
            ErrosValidationResults.Create("OP_03", "La operación que ha intentado crear ya se encuentra para el vuelo y aerolinea en la que ha intentado operar");

        /// <summary>Error: La operación debe estar asociada a un vuelo válido.</summary>
        public static ErrosValidationResults OperationInvalidFlight =>
            ErrosValidationResults.Create("OP_04", "Toda operación debe estar asociada a un vuelo valido, favor verificar el vuelo sobre el cual a intentado operar");

        /// <summary>Error: La operación debe estar asociada a un aeropuerto válido.</summary>
        public static ErrosValidationResults OperationInvalidAirport =>
            ErrosValidationResults.Create("OP_05", "Toda operación debe estar asociada a un aeropuerto valido");

        /// <summary>Error: El vuelo sobre el cual se intenta operar no se encuentra en circulación.</summary>
        public static ErrosValidationResults OperationInvalidFlightCancelled =>
            ErrosValidationResults.Create("OP_06", "El vuelo sobre el cual a intentado hacer un cambio operario no se encuentra en circulación");

        /// <summary>Error: El vuelo sobre el cual se intenta operar no está en una fecha válida.</summary>
        public static ErrosValidationResults OperationInvalidFlightPast =>
            ErrosValidationResults.Create("OP_07", "El vuelo sobre el cual a intentado operar no se encuentra en una fecha valida a  la del flujo operario");

        /// <summary>Error: El tipo de operación ya existe para el vuelo y aerolínea indicados.</summary>
        public static ErrosValidationResults OperationExistType =>
           ErrosValidationResults.Create("OP_03", "El tipo de  operación que ha intentado crear ya se encuentra para el vuelo y aerolinea en la que ha intentado operar");
    }
}
