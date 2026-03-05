using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Aiport { 
    public static class AirportErrors
    {
        public static ErrosValidationResults InvalidAirportCode =>
            ErrosValidationResults.Create("AIRPORT_01", "El código del aeropuerto debe tener exactamente 4 caracteres");

        public static ErrosValidationResults AirportNameRequired =>
            ErrosValidationResults.Create("AIRPORT_02", "El nombre del aeropuerto es obligatorio");

        public static ErrosValidationResults InvalidEmailFormat =>
            ErrosValidationResults.Create("AIRPORT_03", "El formato del email no es válido");

        public static ErrosValidationResults AirportCodeExists =>
            ErrosValidationResults.Create("AIRPORT_04", "Ya existe un aeropuerto registrado con este código");

        public static ErrosValidationResults AirportNotFound =>
            ErrosValidationResults.Create("AIRPORT_05", "El aeropuerto especificado no existe");

        public static ErrosValidationResults AirportInactive =>
            ErrosValidationResults.Create("AIRPORT_06", "El aeropuerto está inactivo");

        public static ErrosValidationResults InvalidApiKey =>
            ErrosValidationResults.Create("AIRPORT_07", "La clave API proporcionada no es válida");

        public static ErrosValidationResults CityRequired =>
            ErrosValidationResults.Create("AIRPORT_08", "La ciudad es obligatoria");

        public static ErrosValidationResults CountryRequired =>
            ErrosValidationResults.Create("AIRPORT_09", "El país es obligatorio");

        public static ErrosValidationResults MaxNameLength =>
            ErrosValidationResults.Create("AIRPORT_10", "El nombre del aeropuerto no puede exceder los 150 caracteres");
    }
}
