using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.DomainService.Interfaces.Flights
{
    public interface IFlightDomainService
    {
        Task<ValidationResult> IsValidOriginAirportAsync(string codeAirlines, string airportCode);
        Task<ValidationResult> IsValidDestinationAirportAsync(string codeAirlines, string airportCode);
        Task<ValidationResult> IsValidStatusTransitionAsync(byte currentStateId, FlightStateEnum newState);
    }
}
