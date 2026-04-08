using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AeroVeloz.Web.Services.Interfaces;
using AeroVeloz.Web.Models.Flights;

namespace AeroVeloz.Web.Pages.SuperAdmin
{
    [Authorize] // Protege la página, requiere login
    public class IndexModel : PageModel
    {
        private readonly IFlightApiService _flightService;

        public IndexModel(IFlightApiService flightService)
        {
            _flightService = flightService;
        }

        public string? UserName { get; set; }
        public string? Role { get; set; }
        public string? Organization { get; set; }
        public string? ErrorMessage { get; set; }
        public List<FlightReadDto> Flights { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Extraer datos de la cookie segura generada en el Login
            UserName = User.Identity?.Name;
            Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            Organization = User.Claims.FirstOrDefault(c => c.Type == "OrganizationName")?.Value;

            // Extraer Token y OrganizationId para consumir la API de Vuelos
            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            var orgIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;
            
            if (!string.IsNullOrEmpty(token) && int.TryParse(orgIdClaim, out int orgId))
            {
                try 
                {
                    Flights = await _flightService.GetFlightsByAirlineAsync("AVX", orgId, token);
                    if (!Flights.Any()) 
                    {
                        ErrorMessage = "La lista de vuelos regresó vacía desde el Backend (0 registros).";
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error al consumir la API: {ex.Message}";
                }
            }
            else 
            {
                ErrorMessage = "El token JWT o el OrganizationId no se encontraron en la sesión.";
            }
        }
    }
}
