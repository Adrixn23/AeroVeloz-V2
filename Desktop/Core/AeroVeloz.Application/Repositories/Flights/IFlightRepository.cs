using AeroVeloz.Application.DTOs.Flights;

namespace AeroVeloz.Application.Repositories.Flights;

public interface IFlightRepository
{
    Task<IReadOnlyCollection<FlightListDto>> GetActiveFlightsForAirportAsync(string airportCode);
    Task<IReadOnlyCollection<FlightDetailsDto>> GetAllActiveFlightsWithDetailsAsync();
    Task<IReadOnlyCollection<FlightDetailsDto>> GetFlightsByAirportWithDetailsAsync(string airportCode);
    Task<FlightDetailsDto?> GetFlightWithDetailsAsync(short flightId);
}
