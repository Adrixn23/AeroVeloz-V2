using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.DomainService.Interfaces.Airlines
{
    public interface IAirlineDomainService
    {
        Task<ValidationResult> IsValidAirlineCodeAsync(string airlineCode);
        Task<ValidationResult> HasConnectionWithAirportAsync(string codeAirlines, string airportCode);
    }
}
