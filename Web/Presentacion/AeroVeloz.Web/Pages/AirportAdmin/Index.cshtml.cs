using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AeroVeloz.Web.Services.Interfaces;
using AeroVeloz.Web.Models.Flights;

namespace AeroVeloz.Web.Pages.AirportAdmin
{
    [Authorize(Roles = "AIRPORTADMIN,SYSTEMADMIN,AIRLINEADMIN")] // Permite a los Airport Admins, Super Admins y AirlineAdmins (para pruebas)
    public class IndexModel : PageModel
    {
        private readonly IFlightApiService _flightService;

        public IndexModel(IFlightApiService flightService)
        {
            _flightService = flightService;
        }

        public string? UserName { get; set; }
        public string? Organization { get; set; }
        public string? ErrorMessage { get; set; }
        public List<FlightReadDto> Flights { get; set; } = new();

        public async Task OnGetAsync()
        {
            UserName = User.Identity?.Name;
            Organization = "AeroVeloz";

            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            
            if (!string.IsNullOrEmpty(token))
            {
                try 
                {
                    // En un caso real, el código de aeropuerto se obtiene del perfil del usuario logueado.
                    // Para propósitos del prototipo web usamos "SKBO" (El Dorado).
                    string airportCode = "SKBO";
                    Flights = await _flightService.GetFlightsByAirportAsync(airportCode, token);
                    
                    if (!Flights.Any()) 
                    {
                        ErrorMessage = $"No hay vuelos activos registrados para el aeropuerto {airportCode}.";
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error al consumir la API de Aeropuerto: {ex.Message}";
                }
            }
            else 
            {
                ErrorMessage = "El token JWT no se encontró en la sesión segura.";
            }
        }
    }
}
