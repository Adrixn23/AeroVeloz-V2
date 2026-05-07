using Microsoft.AspNetCore.Mvc.RazorPages;
using AeroVeloz.Web.Models.Flights;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Pages
{
    public class PublicBoardModel : PageModel
    {
        private readonly IFlightApiService _flightService;

        public PublicBoardModel(IFlightApiService flightService)
        {
            _flightService = flightService;
        }

        public List<FlightReadDto> Flights { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                Flights = await _flightService.GetPublicFlightsAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"No se pudo cargar la pantalla pública: {ex.Message}";
            }
        }
    }
}
