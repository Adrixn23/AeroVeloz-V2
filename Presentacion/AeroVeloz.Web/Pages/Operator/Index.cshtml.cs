using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AeroVeloz.Web.Services.Interfaces;
using AeroVeloz.Web.Models.Flights;

namespace AeroVeloz.Web.Pages.Operator
{
    [Authorize(Roles = "OPERATIONAIRPORT,SYSTEMADMIN,AIRLINEADMIN")] // Añadido SYSTEMADMIN para pruebas
    public class IndexModel : PageModel
    {
        private readonly IFlightApiService _flightService;

        public IndexModel(IFlightApiService flightService)
        {
            _flightService = flightService;
        }

        public string? UserName { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
        public List<FlightReadDto> Flights { get; set; } = new();

        public async Task OnGetAsync()
        {
            UserName = User.Identity?.Name;
            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            var orgIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;
            
            // Leemos mensajes temporales
            SuccessMessage = TempData["SuccessMessage"] as string;
            ErrorMessage = TempData["ErrorMessage"] as string;

            if (!string.IsNullOrEmpty(token) && int.TryParse(orgIdClaim, out int orgId))
            {
                try 
                {
                    // Para el operador, idealmente solo vería los vuelos de su aeropuerto.
                    Flights = await _flightService.GetFlightsByAirportAsync("SKBO", token);
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error de conexión con la API: {ex.Message}";
                }
            }
        }
    }
}
