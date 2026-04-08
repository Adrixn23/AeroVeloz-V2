using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AeroVeloz.Web.Services.Interfaces;
using AeroVeloz.Web.Models.Flights;

namespace AeroVeloz.Web.Pages.SuperAdmin
{
    [Authorize(Roles = "AIRLINEADMIN,SYSTEMADMIN")]
    public class BatchUploadModel : PageModel
    {
        private readonly IFlightApiService _flightService;

        public BatchUploadModel(IFlightApiService flightService)
        {
            _flightService = flightService;
        }

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // En un caso real, aquí procesarías un archivo CSV subido por el usuario.
            // Para el prototipo, simulamos una carga de 2 vuelos.
            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            var orgIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId) || !int.TryParse(orgIdClaim, out int orgId))
            {
                ErrorMessage = "Sesión inválida.";
                return Page();
            }

            var batch = new List<FlightBatchItemDto>
            {
                new FlightBatchItemDto("AVX", "SKBO", "KJFK", DateTimeOffset.UtcNow.AddDays(1), "A2", ""),
                new FlightBatchItemDto("AVX", "KJFK", "SKBO", DateTimeOffset.UtcNow.AddDays(2), "B1", "")
            };

            try
            {
                var result = await _flightService.UploadBatchAsync(batch, userId, orgId, token);
                if (result)
                {
                    SuccessMessage = "Carga masiva procesada con éxito (2 vuelos añadidos).";
                }
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
