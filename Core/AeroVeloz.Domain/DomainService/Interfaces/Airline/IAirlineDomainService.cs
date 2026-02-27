using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.ValidationBase;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.DomainService.Interfaces.Airline
{
    public interface IAirlineDomainService
    {

        Task<ValidationResult> ValidateBatchAsync(IEnumerable<AeroVeloz.Domain.Entities.Flight.Flight> batch, string airportName); // ESTA LINEA ES PARA VALIDAR UN LOTE DE VUELO
        // ES IEnumerable, por que esta recibiendo objetos tipo vuelo, y el airportname es el nombre de la aerolinea que esta procesando la solicitud. 


        ValidationResult ValidateInFlight(AeroVeloz.Domain.Entities.Flight.Flight flight, FlightStateEnum newState);
        // va a validar si paso o fallo, este recibe un unico objeto de tipo vuelo, y recibe el nuevo estado propuesto para ese vuelo, ejmp aterrizado cancelado etc


        Task<ValidationResult> ValidateOwnerAsync(string airlineCode, IEnumerable<AeroVeloz.Domain.Entities.Flight.Flight> batch);
        // nombre de metodo pa validar quien es el dueño del lote, recibe el codigo de la aerolinea que esta intentando enviar o modificar lote,
        // recibe la lista de los vuelos que se quiere procesar

        /* Cubre la parte de seguridad operativa y la regla de no modificar lo ya procesado: No podraa eliminar los lotes de vuelos que envié al aeropuerto.
        Primero, verifica que todos los vuelos en ese batch tengan el mismo codeAirlines que el airlineCode proporcionado(la aerolínea no puede alterar vuelos de otra compañia ta prohibido) de´spues  debe verificar
        prob consultando un repositorioo, que ese lote de vuelos no haya sido ya procesado o cerrado por el equipo de operaciones del aeropuerto */
    }
}
