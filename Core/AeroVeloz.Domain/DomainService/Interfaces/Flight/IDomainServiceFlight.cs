using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.ValidationBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AeroVeloz.Domain.DomainService.Interfaces.Flight
{
    public interface IDomainServiceFlight
    {
        Task<AeroVeloz.Domain.Entities.Flight.Flight> GetFlightidNumber(short id); 

        ValidationResult GetcodeAirlinesOwner(Entities.Flight.Flight flight, string codeAirline); //Valida que el cambio provenga de la aerolinea dueña del vuelo.

        Task<ValidationResult> IsvalidOriginAirport(Entities.Flight.Flight flight); // Valida si el aeropuerto de origen es correcto

       
        Task<AeroVeloz.Domain.Entities.Flight.Flight> ChangeStatedFlightAsync(Entities.Flight.Flight flight, FlightStateEnum newState);  // realiza el cambio de estado, programado, cancelado, etc.

        Task<AeroVeloz.Domain.Entities.Flight.Flight> ChangeBoardingFlightAsync(Entities.Flight.Flight flight, string newGate); //le ordena al objeto vuelo que actualice su puerta interna, y valida el momento de cambiar la puerta 

         Task<IEnumerable<Entities.Flight.Flight>> GetAllFlightsOperational(); // devuelve la lista de vuelos que no termina o sea operacionales







    }
}

