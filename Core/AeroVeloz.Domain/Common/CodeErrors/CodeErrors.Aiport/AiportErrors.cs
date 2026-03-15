
using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Aiport
{

    /// <summary>
    /// Clase estática que centraliza todos los errores de validación relacionados con la gestión de aeropuertos.
    /// Incluye errores de formato de códigos IATA/ICAO, validaciones de datos geográficos,
    /// verificación contra fuentes externas y duplicidad de registros.
    /// </summary>
    public static class AirportErrors
    {

        public static ErrosValidationResults AirportInvalid =>
            ErrosValidationResults.Create("AIRPORT_01", "El Airport que ha intentado crear no cumple con los requerimientos minimos para hacer generado");

        public static ErrosValidationResults AirportCodeMissing =>
            ErrosValidationResults.Create("AIRPORT_02", "El aeropuerto debe tener al menos un código IATA o ICAO válido.");

        public static ErrosValidationResults AirportIataInvalid =>
            ErrosValidationResults.Create("AIRPORT_03", "El código IATA del aeropuerto no tiene un formato válido (3 letras).");

        public static ErrosValidationResults AirportIcaoInvalid =>
            ErrosValidationResults.Create("AIRPORT_04", "El código ICAO del aeropuerto no tiene un formato válido (4 letras).");

        public static ErrosValidationResults  AirportExistInSystem =>
            ErrosValidationResults.Create("AIRPORT_05", "El correo proporcionado para esta organización ya se encuentra registrado en el sistema.");

        public static ErrosValidationResults AirportAlreadyExists =>
            ErrosValidationResults.Create("AIRPORT_06", "El aeropuerto ya existe registrado en la organización.");

        public static ErrosValidationResults AirportCountryInvalid =>
            ErrosValidationResults.Create("AIRPORT_07", "El país del aeropuerto es inválido o está vacío.");

        public static ErrosValidationResults AirportCityInvalid =>
            ErrosValidationResults.Create("AIRPORT_08", "La ciudad del aeropuerto es inválida o está vacía.");


    }
}
