using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Operations
{
    public static class OperationalChangeErrors
    {
        public static ErrosValidationResults InvalidaOperationByOrganization =>
             ErrosValidationResults.Create("OP_01", "La operación que ha intentando realizar no es valida para este organismo " +
                 "ya que el mismo no fue encontrado o se encuentra desactivado, favor confirmar el estado operativo de dicho organismo");
        public static ErrosValidationResults InvalidOperation =>
            ErrosValidationResults.Create("OP_02", "La operación que se ha intentado crear no cumple con los requerimientos minimos para ser creada");
        public static ErrosValidationResults OperationExist =>
            ErrosValidationResults.Create("OP_03", "La operación que ha intentado crear ya se encuentra para el vuelo y aerolinea en la que ha intentado operar");
        public static ErrosValidationResults OperationInvalidFlight =>
            ErrosValidationResults.Create("OP_04", "Toda operación debe estar asociada a un vuelo valido, favor verificar el vuelo sobre el cual a intentado operar");
        public static ErrosValidationResults OperationInvalidAirport =>
            ErrosValidationResults.Create("OP_05", "Toda operación debe estar asociada a un aeropuerto valido");
        public static ErrosValidationResults OperationInvalidFlightCancelled =>
            ErrosValidationResults.Create("OP_06", "El vuelo sobre el cual a intentado hacer un cambio operario no se encuentra en circulación");
        public static ErrosValidationResults OperationInvalidFlightPast =>
            ErrosValidationResults.Create("OP_07", "El vuelo sobre el cual a intentado operar no se encuentra en una fecha valida a  la del flujo operario");

        public static ErrosValidationResults OperationExistType =>
           ErrosValidationResults.Create("OP_08", "El tipo de  operación que ha intentado crear ya se encuentra para el vuelo y aerolinea en la que ha intentado operar");
    }
}
