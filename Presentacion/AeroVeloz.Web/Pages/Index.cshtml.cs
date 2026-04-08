using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AeroVeloz.Web.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                if (User.IsInRole("AIRPORTADMIN"))
                    return RedirectToPage("/AirportAdmin/Index");
                
                if (User.IsInRole("AIRLINEADMIN") || User.IsInRole("SUPERADMIN"))
                    return RedirectToPage("/SuperAdmin/Index");
            }
            
            return RedirectToPage("/Auth/Login");
        }
    }
}
