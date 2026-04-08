using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AeroVeloz.Web.Services.Interfaces;
using AeroVeloz.Web.Models.Flights;

namespace AeroVeloz.Web.Pages.Operator
{
    [Authorize(Roles = "OPERATIONAIRPORT,SYSTEMADMIN,AIRLINEADMIN")]
    public class UpdateStateModel : PageModel
    {
        private readonly IFlightApiService _flightService;

        public UpdateStateModel(IFlightApiService flightService)
        {
            _flightService = flightService;
        }

        [BindProperty]
        public FlightUpdateStateDto Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public void OnGet(short flightNumber, string airline)
        {
            Input.FlightNumber = flightNumber;
            Input.CodeAirlinesIcao = airline;
            // Estado por defecto seleccionado para pruebas
            Input.FlightStateId = 2; // Boarding
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            var orgIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId) || !int.TryParse(orgIdClaim, out int orgId))
            {
                ErrorMessage = "La sesión no es válida o está incompleta.";
                return Page();
            }

            try
            {
                // Invocación a la API para hacer un PUT Request (Requisito de la rúbrica CRUD vía API)
                var result = await _flightService.UpdateFlightStateAsync(Input, userId, orgId, token);
                
                if (result)
                {
                    TempData["SuccessMessage"] = $"Estado del Vuelo {Input.FlightNumber} actualizado a ID {Input.FlightStateId}.";
                    return RedirectToPage("/Operator/Index");
                }
                
                return Page();
            }
            catch (ApplicationException ex)
            {
                // Este catch atrapa los 400 Bad Request o reglas del dominio (como FLIGHT_BACKWARD)
                ErrorMessage = ex.Message;
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error del sistema: {ex.Message}";
                return Page();
            }
        }
    }
}
