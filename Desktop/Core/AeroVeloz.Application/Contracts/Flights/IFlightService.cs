using AeroVeloz.Application.DTOs.Flights;
using AeroVeloz.Application.Handlers.Result;

namespace AeroVeloz.Application.Contracts.Flights
{
    public interface IFlightService
    {
        Task<OperationResult<IReadOnlyCollection<FlightDetailsDto>>> GetAllActiveFlightsAsync(Guid userId, int orgId);
        Task<OperationResult<IReadOnlyCollection<FlightDetailsDto>>> GetFlightsByAirportAsync(string airportCode, Guid userId, int orgId);
        Task<OperationResult<FlightDetailsDto>> GetFlightDetailsAsync(short flightId, Guid userId, int orgId);
    }
}
