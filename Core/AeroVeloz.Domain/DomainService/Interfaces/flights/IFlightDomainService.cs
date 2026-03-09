using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flights; 

namespace AeroVeloz.Domain.DomainService.Interfaces.Flight
{
    public interface IFlightDomainService
        {
        // este verifica si el aeropuerto de origen es vlido
        Task<ValidationResult> IsValidOriginAirportAsync(string airportCode);

        Task<ValidationResult> IsValidDestinationAirportAsync(string airportCode);// Verifica si el aeropuerto de destino es válido

        // Genera y verifica que el número de vuelo no esté en uso
        Task<short> GetFlightIdNumberAsync(string airlineCode);

        Task<ValidationResult> IsValidStatusTransitionAsync(Entities.Flights.Flight flight, FlightStateEnum newStatus); // verifica si la transicion de estado es valido
    }
}
