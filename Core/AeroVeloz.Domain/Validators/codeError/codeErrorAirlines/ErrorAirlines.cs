using AeroVeloz.Domain.ValidationBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.Validators.codeError.codeError_Airlines
{
    public static class ErrorAirlines
    {

        public static DomainError InvalidAirlineCode =>
            DomainError.Create("Airline_01", "el codigo no tiene un formato valido o está Vacio");


        public static DomainError InvalidAirlineName =>
            DomainError.Create("Airline_02", "el Nombre de la aerolinea debe ser Obligatorio.");

        public static DomainError InvalidBatchAirline =>
            DomainError.Create("Airline_03", "incongruencia: los aeropuertos del lote de vuelo no coinciden con el aeropuerto receptor. ");


        public static DomainError InvalidCancellationInFlight =>
            DomainError.Create("Airline_04", "no se puede cancelar un vuelo que ya ha despegado");

        public static DomainError InvalidUnauthorizedBatchAccess =>
            DomainError.Create("Airline_05", "La aerolinea no es dueña de este lote de vuelos, o este lote ya fue procesado.");

        public static DomainError MissingIataCode =>
     DomainError.Create("Airline_06", "El código IATA es obligatorio para registrar o procesar operaciones de una aerolínea.");

        public static DomainError InvalidIataFormat =>
    DomainError.Create("Airline_07", "El código IATA de la aerolinea debe tener minimo 3 caracteres alfanumericos ");




    }
}
