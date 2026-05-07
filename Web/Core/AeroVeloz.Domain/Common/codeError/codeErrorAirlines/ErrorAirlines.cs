using AeroVeloz.Domain.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.Common.codeError.codeErrorAirlines
{
    public static class ErrorAirlines
    {

        public static ErrosValidationResults InvalidAirlineCode =>
            ErrosValidationResults.Create("Airline_01", "el codigo no tiene un formato valido o está Vacio");


        public static ErrosValidationResults InvalidAirlineName =>
            ErrosValidationResults.Create("Airline_02", "el Nombre de la aerolinea debe ser Obligatorio.");

        public static ErrosValidationResults InvalidBatchCoherence =>
            ErrosValidationResults.Create("Airline_03", "incongruencia: los aeropuertos del lote de vuelo no coinciden con el aeropuerto receptor. ");

     
        public static ErrosValidationResults InvalidCancellationInFlight =>
            ErrosValidationResults.Create("Airline_04", "no se puede cancelar un vuelo que ya ha despegado o este en estado Final. ");

        public static ErrosValidationResults UnauthorizedBatchAccess =>
            ErrosValidationResults.Create("Airline_05", "La aerolinea no es dueña de este lote de vuelos, o este lote ya fue procesado.");

        public static ErrosValidationResults MissingIataCode =>
     ErrosValidationResults.Create("Airline_06", "El código IATA es obligatorio para registrar o procesar operaciones de una aerolínea.");

        public static ErrosValidationResults InvalidIataFormat =>
    ErrosValidationResults.Create("Airline_07", "El código IATA de la aerolinea debe tener minimo 3 caracteres alfanumericos ");

        public static ErrosValidationResults InvalidRevertFlightAirline =>
    ErrosValidationResults.Create("Flight_08", "No se puede revertir un vuelo al estado Programado una vez iniciado el proceso.");

        public static ErrosValidationResults FlightAlreadyFinalized =>
ErrosValidationResults.Create("Flight_09", "el vuelo ya esta finalizado.");

        public static ErrosValidationResults InvalidFlightTimeline =>
   ErrosValidationResults.Create("Flight_10", "La hora de llegada no puede ser anterior o igual a la hora de salida");





    }
}
