using AeroVeloz.Desktop.Models.DTOs.Flight;

namespace AeroVeloz.Desktop.Services.Interfaces.Airport;

public interface IFlightService
{
    Task<IEnumerable<FlightForOperationDto>> GetFlightsForOperationsAsync();
    Task<IEnumerable<FlightForOperationDto>> GetFlightsByAirportAsync(string airportCode);
    Task<FlightForOperationDto?> GetFlightDetailsAsync(short flightId);
    Task<IEnumerable<FlightOperationDto>> GetFlightOperationsAsync(short flightId);
}
