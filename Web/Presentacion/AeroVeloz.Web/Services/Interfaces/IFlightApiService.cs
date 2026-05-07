using AeroVeloz.Web.Models.Flights;

namespace AeroVeloz.Web.Services.Interfaces
{
    public interface IFlightApiService
    {
        Task<List<FlightReadDto>> GetFlightsByAirlineAsync(string airlineCode, int orgId, string token);
        Task<List<FlightReadDto>> GetFlightsByAirportAsync(string airportCode, string token);
        Task<List<FlightReadDto>> GetPublicFlightsAsync();
        Task<FlightReadDto?> GetFlightDetailAsync(short flightNumber, string airlineCode, string token);
        Task<bool> UpdateFlightStateAsync(FlightUpdateStateDto dto, string userId, int orgId, string token);
        Task<bool> UploadBatchAsync(List<FlightBatchItemDto> batch, string userId, int orgId, string token);
    }
}
