using AeroVeloz.Web.Models.Flights;

namespace AeroVeloz.Web.Services.Interfaces
{
    public interface IFlightApiService
    {
        Task<List<FlightReadDto>> GetFlightsByAirlineAsync(string airlineCode, int orgId, string token);
    }
}
