using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AeroVeloz.Web.Models.Flights;
using AeroVeloz.Web.Models.Subscriptions;
using AeroVeloz.Web.Services.Interfaces;
using System.Security.Claims;

namespace AeroVeloz.Web.Pages.Flights
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IFlightApiService _flightService;
        private readonly ISubscriptionApiService _subscriptionService;

        public DetailsModel(IFlightApiService flightService, ISubscriptionApiService subscriptionService)
        {
            _flightService = flightService;
            _subscriptionService = subscriptionService;
        }

        public FlightReadDto? Flight { get; set; }
        public int InterestedCount { get; set; }
        public List<SubscriptionReadDto> Subscriptions { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(short flightNumber, string airline)
        {
            var token = User.Claims.FirstOrDefault(c => c.Type == "JwtToken")?.Value;
            if (string.IsNullOrEmpty(token)) return RedirectToPage("/Auth/Login");

            try
            {
                Flight = await _flightService.GetFlightDetailAsync(flightNumber, airline, token);
                
                if (Flight == null) return NotFound();

                InterestedCount = await _subscriptionService.GetSubscriptionCountAsync(flightNumber, airline, token);
                Subscriptions = await _subscriptionService.GetSubscriptionsByFlightAsync(flightNumber, airline, token);
                
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostSubscribeAsync(short flightNumber, string airline, string email)
        {
            if (string.IsNullOrEmpty(email)) return Page();

            var result = await _subscriptionService.SubscribeExternalAsync(email, flightNumber, airline);
            if (result)
            {
                TempData["SuccessMessage"] = "Te has suscrito correctamente a las notificaciones.";
            }
            else
            {
                TempData["ErrorMessage"] = "No se pudo realizar la suscripción.";
            }

            return RedirectToPage(new { flightNumber, airline });
        }
    }
}
