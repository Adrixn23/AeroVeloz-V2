using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flights;

namespace AeroVeloz.Domain.DomainService.Interfaces.Flights
{
    public interface IFlightIngestionDomainService
    {
        Task<ValidationResult> ValidateFlightRowAsync(Flight flight, string codeAirlines);
    }
}
