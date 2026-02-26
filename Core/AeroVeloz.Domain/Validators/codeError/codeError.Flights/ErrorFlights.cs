using AeroVeloz.Domain.Common;
using AeroVeloz.Domain.ValidationBase;

namespace AeroVeloz.Domain.Validators.codeError.codeError.Flights
{
   public static class ErrorFlights
{
    public static DomainError InvalidIdFlight =>
        DomainError.Create("Flight_01", "El Id de vuelo debe ser un identificador válido.");

    public static DomainError InvalidCodeAirlines =>
        DomainError.Create("Flight_02", "El código de la aerolínea no tiene un formato válido.");

    public static DomainError InvalidFlightState =>
  DomainError.Create("Flight_03", "La transición del estado actual al nuevo no es permitida en este momento.");

    public static DomainError SameOriginAndDestination =>
        DomainError.Create("Flight_04", "El aeropuerto de origen y destino no pueden ser el mismo.");

    
    public static DomainError DepartureInPast =>
        DomainError.Create("Flight_05", "No se puede programar un vuelo con una fecha de salida en el pasado.");

    public static DomainError ArrivalBeforeDeparture =>
        DomainError.Create("Flight_06", "La fecha de llegada debe ser posterior a la fecha de salida.");

    public static DomainError DepartureRequired =>
        DomainError.Create("Flight_07", "La fecha de salida programada es obligatoria para crear el vuelo.");

   
    public static DomainError InvalidBoardingGate =>
        DomainError.Create("Flight_08", "La puerta de embarque proporcionada no es válida.");

    public static DomainError InvalidArrivalGate =>
        DomainError.Create("Flight_09", "La puerta de llegada no puede ser asignada antes del aterrizaje o es inválida.");

        public static DomainError InvalidOrigin =>
            DomainError.Create("Flight_10", "El aeropuerto de origen no es válido");

        public static DomainError InvalidOwner =>
            DomainError.Create("Flight_11", "La aerolínea no es dueña de este vuelo.");


}

    }

