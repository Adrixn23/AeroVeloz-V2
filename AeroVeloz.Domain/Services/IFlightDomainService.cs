using AeroVeloz.Domain.Entities.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.Services
{
    public interface  IFlightDomainService
    {
        
        
        Task<bool> FlightExistsAsync(string codeAirlines); // para ver si existe el vuelo qu se esta reciiendo

        bool IsvalidforCreation(Flight flight); // es para verificar si el vuelo es valido ejm cuenta con los parametros de la creacion

        Task<bool> IsFromValidAirLineAsync(string AirLlineCode); // proviene de una aerolinea valida???

        bool IsStateChangeValid(Flight flight, byte newStateId);

        bool IsChangeAuthorizedByAirline(Flight flight, string airlineCode); // el cambio de estado proviene de la aerolinea a la que pertenece

        IEnumerable<Flight> FilterOperationalFlights(IEnumerable<Flight> flights); // filtraje de vueloos que se encuentren unicamente en estados operativos

    }
}
