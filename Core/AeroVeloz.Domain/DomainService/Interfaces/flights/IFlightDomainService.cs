using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flights; 

namespace AeroVeloz.Domain.DomainService.Interfaces.Flight
{
    public interface IFlightDomainService
        {
        // este verifica si el aeropuerto de origen es vlido
        Task<ValidationResult> IsValidOriginAirportAsync(string airportCode);

        Task<ValidationResult> IsValidDestinationAirportAsync(string airportCode);// Verifica si el aeropuerto de destino es v�lido

        // Genera y verifica que el n�mero de vuelo no est� en uso
        Task<short> GetFlightIdNumberAsync(string airlineCode);

        Task<ValidationResult> IsValidStatusTransitionAsync(Entities.Flights.Flight flight, short newStatus); // verifica si la transicion de estado es valido
        Task<bool> IsAirlineOwnerOfFlightAsync(short id, string? codeAirlines);
    }
}
