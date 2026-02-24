using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.Operations
{
    public static class OperationalChangeErrors  
    {
        public static DomainError InvalidFlightNumber =>
            DomainError.Create("OP_CHANGE_01", "El número de vuelo hacer modificado debe ser mayor a zero");
        public static DomainError FutreChangeDate =>
            DomainError.Create("OP_CHANGE_02", "La fecha debe estar en el mismo flujo operacional que el state del vuelo");
        public static DomainError InvalidAirlineCode =>
            DomainError.Create("OP_CHANGE_03", "El codigo de la aerolinea debe tener exatacmente 3 caracteres");
        public static DomainError InvalidActorRef =>
            DomainError.Create("OP_CHANGE_04", "El cambio de coincidir a un actor del sistema aeropuertuario valido");
        public static DomainError CauseRequiered =>
            DomainError.Create("OP_CHANGE_05", "Se debe colocar una causa de cambio operacional obligatoria");
        public static DomainError InvalidChangeOperational =>
            DomainError.Create("OP_CHANGE_06", "El cambio operacional que ha intentado realizar no es valido para el state del vuelo actual");
        public static DomainError InvalidChangeOperationDateInvalidPast =>
            DomainError.Create("OP_CHANGE_07", "El cambio operacional que se ha intentado corresponde a un" +
                "registro antiguo por lo que no puede se realizado, consulte la fecha del vuelo que ha intentado modificar");
    }
}
