using AeroVeloz.Web.Models.Auth;
using AeroVeloz.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AeroVeloz.Web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        [BindProperty]
        public LoginRequestDto Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnGet()
        {
            // Si el usuario ya está autenticado, redirigirlo al Dashboard correspondiente
            if (User.Identity?.IsAuthenticated == true)
            {
                RedirectToDashboard();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validación de Frontend (Distribuida)
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Consumo del Servicio API Backend
                var response = await _authService.LoginAsync(Input);

                if (response != null && response.User != null && !string.IsNullOrEmpty(response.AccessToken))
                {
                    // Crear los Claims (Datos de la sesión segura del usuario)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, response.User.UserName),
                        new Claim(ClaimTypes.NameIdentifier, response.User.UserId.ToString()),
                        new Claim(ClaimTypes.Role, response.User.RoleName),
                        new Claim("OrganizationId", response.User.OrganizationId.ToString()),
                        new Claim("OrganizationName", response.User.OrganizationName),
                        new Claim("JwtToken", response.AccessToken) // Guardamos el JWT para inyectarlo después
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = response.ExpiresAtUtc
                    };

                    // Guardar Cookie Segura (Autenticación Automática del Sistema Web)
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToDashboard();
                }

                ErrorMessage = "Credenciales incorrectas o usuario inactivo.";
                return Page();
            }
            catch (ApplicationException ex)
            {
                // Manejo de errores: API Caída
                ErrorMessage = ex.Message;
                return Page();
            }
        }

        private IActionResult RedirectToDashboard()
        {
            // Redirección basada en el Rol (Heurística de UX)
            if (User.IsInRole("AIRPORTADMIN"))
                return RedirectToPage("/AirportAdmin/Index");
            
            if (User.IsInRole("OPERATIONAIRPORT"))
                return RedirectToPage("/Operator/Index");
            
            if (User.IsInRole("AIRLINEADMIN"))
                return RedirectToPage("/SuperAdmin/Index"); // O donde decidas enviarlo
                
            if (User.IsInRole("SYSTEMADMIN"))
                return RedirectToPage("/SuperAdmin/Index");

            return RedirectToPage("/Index");
        }
    }
}
