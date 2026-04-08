using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AeroVeloz.Web.Models.Users;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Pages.AirportAdmin
{
    [Authorize(Roles = "AIRPORTADMIN,SYSTEMADMIN,AIRLINEADMIN")]
    public class StaffModel : PageModel
    {
        private readonly IUserApiService _userService;

        public StaffModel(IUserApiService userService)
        {
            _userService = userService;
        }

        public List<UserStaffDto> StaffList { get; set; } = new();

        [BindProperty]
        public CreateStaffDto NewStaff { get; set; } = new();

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            var orgIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;

            SuccessMessage = TempData["SuccessMessage"] as string;
            ErrorMessage = TempData["ErrorMessage"] as string;

            if (!string.IsNullOrEmpty(token) && int.TryParse(orgIdClaim, out int orgId))
            {
                StaffList = await _userService.GetStaffByOrgAsync(orgId, token);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            var orgIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;

            if (string.IsNullOrEmpty(token) || !int.TryParse(orgIdClaim, out int orgId))
                return Page();

            NewStaff.OrganizationId = orgId;
            NewStaff.RoleId = 4; // OPERATIONAIRPORT

            var result = await _userService.CreateStaffAsync(NewStaff, token);
            if (result)
            {
                TempData["SuccessMessage"] = "Operador creado con éxito.";
                return RedirectToPage();
            }

            ErrorMessage = "No se pudo crear el usuario.";
            return Page();
        }
    }
}
