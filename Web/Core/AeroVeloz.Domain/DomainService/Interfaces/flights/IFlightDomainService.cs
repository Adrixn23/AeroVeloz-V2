using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;

using AeroVeloz.Domain.Entities.Flights; 


namespace AeroVeloz.Domain.DomainService.Interfaces.Flights
{
    public interface IFlightDomainService

        {



        Task<ValidationResult> IsValidOriginAirportAsync(string codeAirlines, string airportCode);

        Task<ValidationResult> IsValidDestinationAirportAsync(string codeAirlines, string airportCode);
       
        Task<ValidationResult> IsValidStatusTransitionAsync(byte flightStateId, FlightStateEnum newFlightStateId);
    }
}
