using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AeroVeloz.Web.Models.Airlines;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Pages.AirportAdmin
{
    [Authorize(Roles = "AIRPORTADMIN,SYSTEMADMIN,AIRLINEADMIN")]
    public class AirlinesModel : PageModel
    {
        private readonly IAirlineApiService _airlineService;

        public AirlinesModel(IAirlineApiService airlineService)
        {
            _airlineService = airlineService;
        }

        public List<AirlineReadDto> Airlines { get; set; } = new();

        [BindProperty]
        public AirlineSaveDto NewAirline { get; set; } = new();

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public async Task OnGetAsync()
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            
            SuccessMessage = TempData["SuccessMessage"] as string;
            ErrorMessage = TempData["ErrorMessage"] as string;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    Airlines = await _airlineService.GetAllAirlinesAsync(token);
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Error al obtener aerolíneas: {ex.Message}";
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var orgIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId) || !int.TryParse(orgIdClaim, out int orgId))
            {
                ErrorMessage = "Error de sesión. No se encontraron las credenciales seguras.";
                return Page();
            }

            try
            {
                // Consumir el POST /api/airlines (CRUD)
                var result = await _airlineService.CreateAirlineAsync(NewAirline, token, userId, orgId);
                if (result)
                {
                    TempData["SuccessMessage"] = $"Aerolínea {NewAirline.CodeAirlinesIcao} autorizada correctamente.";
                    return RedirectToPage();
                }
                return Page();
            }
            catch (ApplicationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error interno: {ex.Message}";
                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(string codeIcao)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var orgIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId) || !int.TryParse(orgIdClaim, out int orgId))
            {
                return RedirectToPage();
            }

            try
            {
                var result = await _airlineService.DeleteAirlineAsync(codeIcao, token, userId, orgId);
                if (result)
                {
                    TempData["SuccessMessage"] = $"Aerolínea {codeIcao} eliminada correctamente.";
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"No se pudo eliminar: {ex.Message}";
                return RedirectToPage();
            }
        }
    }
}
