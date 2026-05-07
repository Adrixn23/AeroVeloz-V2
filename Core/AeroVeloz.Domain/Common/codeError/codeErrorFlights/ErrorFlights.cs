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

 

    public static ErrosValidationResults DepartureRequired =>
        ErrosValidationResults.Create("Flight_06", "La fecha de salida programada es obligatoria para crear el vuelo.");

        public static ErrosValidationResults InvalidBoardingGate =>
    ErrosValidationResults.Create("Flight_07", "La puerta de embarque proporcionada no es válida.");

        public static ErrosValidationResults InvalidOrigin =>
                ErrosValidationResults.Create("Flight_08", "El aeropuerto de origen no es válido.");

           
            public static ErrosValidationResults InvalidDestination =>
                ErrosValidationResults.Create("Flight_09", "El aeropuerto de destino no es válido.");

            public static ErrosValidationResults InvalidOwner =>
                ErrosValidationResults.Create("Flight_10", "La aerolínea no es dueña de este vuelo.");

            public static ErrosValidationResults FlightNotFound =>
                ErrosValidationResults.Create("Flight_11", "Vuelo no encontrado en el sistema.");
        public static ErrosValidationResults InvalidArrivalGate =>
        ErrosValidationResults.Create("Flight_12", "La puerta de llegada proporcionada no es válida.");

    }

    }

