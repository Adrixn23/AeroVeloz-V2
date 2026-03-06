using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Aiport
{
    public static class ConnectionAirportErrors
    {

        public static ErrosValidationResults ConnectionInvalid =>
            ErrosValidationResults.Create("AIRPORT_CONNECTION_01", "El Aeropuerto al que ha intantado establecer una comunicación no se encuentra en el sistema o esta desactivado");

        public static ErrosValidationResults ConnectionNoExist =>
            ErrosValidationResults.Create("AIRPORT_CONNECTION_02", "Este aeropuerto no tiene comunicación con ninguna aerolinea");
        public static ErrosValidationResults

    }
}
