using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Airlines;


namespace AeroVeloz.Domain.DomainService.Interfaces.Airlines
{
    public interface IAirlineDomainService
    {
        // Verifica que el código de aerolínea sea válido
        Task<ValidationResult> IsValidAirlineCodeAsync(string airlineCode);

        // Valida que el lote de vuelos cumpla los requisitos mínimos
        Task<ValidationResult> ValidateFlightBatchAsync(Airline entity);

        // Valida que un vuelo ya despegado no reciba un estado de cancelación
        Task<ValidationResult> IsValidStateChangeForActiveFlightAsync(Airline entity, string newStatus);


        // Valida que el lote tenga coherencia con el aeropuerto de salida o llegada
        Task<ValidationResult> IsValidAirportCoherenceAsync(Airline entity, string airportCode);







    }
}
