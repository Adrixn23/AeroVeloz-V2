using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Operations
{
    public static class OperationalChangeErrors  
    {
        public static ErrosValidationResults InvalidFlightNumber =>
            ErrosValidationResults.Create("OP_CHANGE_01", "El número de vuelo hacer modificado debe ser mayor a zero");
        public static ErrosValidationResults FutreChangeDate =>
            ErrosValidationResults.Create("OP_CHANGE_02", "La fecha debe estar en el mismo flujo operacional que el state del vuelo");
        public static ErrosValidationResults InvalidAirlineCode =>
            ErrosValidationResults.Create("OP_CHANGE_03", "El codigo de la aerolinea debe tener exatacmente 3 caracteres");
        public static ErrosValidationResults InvalidActorRef =>
            ErrosValidationResults.Create("OP_CHANGE_04", "El cambio de coincidir a un actor del sistema aeropuertuario valido");
        public static ErrosValidationResults CauseRequiered =>
            ErrosValidationResults.Create("OP_CHANGE_05", "Se debe colocar una causa de cambio operacional obligatoria");
        public static ErrosValidationResults InvalidChangeOperational =>
            ErrosValidationResults.Create("OP_CHANGE_06", "El cambio operacional que ha intentado realizar no es valido para el state del vuelo actual");
        public static ErrosValidationResults InvalidChangeOperationDateInvalidPast =>
            ErrosValidationResults.Create("OP_CHANGE_07", "El cambio operacional que se ha intentado corresponde a un" +
                "registro antiguo por lo que no puede se realizado, consulte la fecha del vuelo que ha intentado modificar");
    }
}
