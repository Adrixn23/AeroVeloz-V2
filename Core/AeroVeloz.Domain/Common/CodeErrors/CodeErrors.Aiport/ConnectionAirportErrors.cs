using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Aiport
{
    public static class ConnectionAirportErrors
    {
        public static ErrosValidationResults ConnectionInvalid =>
            ErrosValidationResults.Create("AIRPORT_CONNECTION_01", "El Aeropuerto al que ha intentado establecer una comunicación no se encuentra en el sistema o está desactivado.");

        public static ErrosValidationResults ConnectionNoExist =>
            ErrosValidationResults.Create("AIRPORT_CONNECTION_02", "Este aeropuerto no tiene comunicación con ninguna aerolínea.");

        public static ErrosValidationResults ConnectionInvalidObject =>
            ErrosValidationResults.Create("AIRPORT_CONNECTION_03", "El objeto de conexión es inválido o nulo.");

        public static ErrosValidationResults ConnectionMissingAirlineCode =>
            ErrosValidationResults.Create("AIRPORT_CONNECTION_04", "El código de la aerolínea es obligatorio para crear la conexión.");

        public static ErrosValidationResults ConnectionMissingAirportCode =>
            ErrosValidationResults.Create("AIRPORT_CONNECTION_05", "El código del aeropuerto es obligatorio para crear la conexión.");

        public static ErrosValidationResults ConnectionAlreadyExists =>
            ErrosValidationResults.Create("AIRPORT_CONNECTION_06", "La conexión entre el aeropuerto y la aerolínea ya existe.");

    }
}
