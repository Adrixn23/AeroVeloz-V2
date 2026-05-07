using System.Net.Http.Headers;
using AeroVeloz.Web.Models.Subscriptions;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Services.Implementations
{
    public class SubscriptionApiService : ISubscriptionApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubscriptionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> GetSubscriptionCountAsync(short flightNumber, string airlineCode, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"api/subscriptions/flight/{flightNumber}/{airlineCode}/count");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<int>>();
                    return result?.Value ?? 0;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<List<SubscriptionReadDto>> GetSubscriptionsByFlightAsync(short flightNumber, string airlineCode, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"api/subscriptions/flight/{flightNumber}/{airlineCode}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<SubscriptionReadDto>>>();
                    return result?.Value ?? new List<SubscriptionReadDto>();
                }
                return new List<SubscriptionReadDto>();
            }
            catch { return new List<SubscriptionReadDto>(); }
        }

        public async Task<bool> SubscribeExternalAsync(string email, short flightNumber, string airlineCode)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                var body = new
                {
                    FlightNumber = flightNumber,
                    CodeAirlines = airlineCode,
                    ContactValue = email,
                    CodeChannel = (byte)1 // 1 = Email
                };

                var response = await client.PostAsJsonAsync("api/subscriptions/external", body);
                
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                    throw new ApplicationException(error?.Message ?? "Error desconocido en la API de suscripciones.");
                }

                return true;
            }
            catch (ApplicationException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error de conexión: {ex.Message}");
            }
        }

        public async Task<bool> CancelSubscriptionAsync(Guid id, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.DeleteAsync($"api/subscriptions/{id}");
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}
