using AeroVeloz.Domain.Common.ValidationBase;

namespace AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.Airport
{
    public static class AirportErrors
    {
        public static DomainError InvalidAirportCode =>
            DomainError.Create("AIRPORT_01", "El código del aeropuerto debe tener exactamente 4 caracteres");

        public static DomainError AirportNameRequired =>
            DomainError.Create("AIRPORT_02", "El nombre del aeropuerto es obligatorio");

        public static DomainError InvalidEmailFormat =>
            DomainError.Create("AIRPORT_03", "El formato del email no es válido");

        public static DomainError AirportCodeExists =>
            DomainError.Create("AIRPORT_04", "Ya existe un aeropuerto registrado con este código");

        public static DomainError AirportNotFound =>
            DomainError.Create("AIRPORT_05", "El aeropuerto especificado no existe");

        public static DomainError AirportInactive =>
            DomainError.Create("AIRPORT_06", "El aeropuerto está inactivo");

        public static DomainError InvalidApiKey =>
            DomainError.Create("AIRPORT_07", "La clave API proporcionada no es válida");

        public static DomainError CityRequired =>
            DomainError.Create("AIRPORT_08", "La ciudad es obligatoria");

        public static DomainError CountryRequired =>
            DomainError.Create("AIRPORT_09", "El país es obligatorio");

        public static DomainError MaxNameLength =>
            DomainError.Create("AIRPORT_10", "El nombre del aeropuerto no puede exceder los 150 caracteres");

     

    }

}
