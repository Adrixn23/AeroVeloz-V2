using AeroVeloz.Domain.Common;
using AeroVeloz.Domain.Common.Validation;


namespace AeroVeloz.Domain.Common.codeError.codeErrorFlights
{
   public static class ErrorFlights
{
    public static ErrosValidationResults InvalidIdFlight =>
        ErrosValidationResults.Create("Flight_01", "El Id de vuelo debe ser un identificador válido.");

    public static ErrosValidationResults InvalidCodeAirlines =>
        ErrosValidationResults.Create("Flight_02", "El código de la aerolínea no tiene un formato válido.");

    public static ErrosValidationResults InvalidFlightState =>
  ErrosValidationResults.Create("Flight_03", "La transición del estado actual al nuevo no es permitida en este momento.");

    public static ErrosValidationResults SameOriginAndDestination =>
        ErrosValidationResults.Create("Flight_04", "El aeropuerto de origen y destino no pueden ser el mismo.");

    
    public static ErrosValidationResults DepartureInPast =>
        ErrosValidationResults.Create("Flight_05", "No se puede programar un vuelo con una fecha de salida en el pasado.");

    public static ErrosValidationResults ArrivalBeforeDeparture =>
        ErrosValidationResults.Create("Flight_06", "La fecha de llegada debe ser posterior a la fecha de salida.");

    public static ErrosValidationResults DepartureRequired =>
        ErrosValidationResults.Create("Flight_07", "La fecha de salida programada es obligatoria para crear el vuelo.");

   
    public static ErrosValidationResults InvalidBoardingGate =>
        ErrosValidationResults.Create("Flight_08", "La puerta de embarque proporcionada no es válida.");

    public static ErrosValidationResults InvalidArrivalGate =>
        ErrosValidationResults.Create("Flight_09", "La puerta de llegada no puede ser asignada antes del aterrizaje o es inválida.");

        public static ErrosValidationResults InvalidOrigin =>
            ErrosValidationResults.Create("Flight_10", "El aeropuerto de origen no es válido");

        public static ErrosValidationResults InvalidOwner =>
            ErrosValidationResults.Create("Flight_11", "La aerolínea no es dueña de este vuelo.");

        public static ErrosValidationResults FlightNotFound => ErrosValidationResults.Create("Flight_12", "Vuelo no encontrado en el sistema");


    }

    }

