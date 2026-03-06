using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.ValidationBase;


namespace AeroVeloz.Domain.DomainService.Interfaces.Flight
{
    public interface IFlightDomainService
    {
        Task<AeroVeloz.Domain.Entities.Flight.Flights> GetFlightidNumber(short id, string airlineCode,int idOrganization); // SI LA AEROLINEA PERTENECE A LA ORGANIZACION
                                                                                                                           // Y SI LA MISMA SE ENCUENTRA ACTIVA, VIENE DEL MOD DE USUARIO.


        ValidationResult GetcodeAirlinesOwner(Entities.Flight.Flights flight, string codeAirline); //Valida que el cambio provenga de la aerolinea dueña del vuelo.

        Task<ValidationResult> IsvalidOriginAirport(Entities.Flight.Flights flight); // Valida si el aeropuerto de origen es correcto


        Task<AeroVeloz.Domain.Entities.Flight.Flights> ChangeStatedFlightAsync(Entities.Flight.Flights flight, FlightStateEnum newState);  // realiza el cambio de estado, programado, cancelado, etc.

        Task<AeroVeloz.Domain.Entities.Flight.Flights> ChangeBoardingFlightAsync(Entities.Flight.Flights flight, string newGate); //le ordena al objeto vuelo que actualice su puerta interna, y valida el momento de cambiar la puerta 

        Task<IEnumerable<Entities.Flight.Flights>> GetAllFlightsOperational(); // devuelve la lista de vuelos que no termina o sea operacionales

    }
}
