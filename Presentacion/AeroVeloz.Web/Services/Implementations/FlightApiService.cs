using System.Net.Http.Headers;
using AeroVeloz.Web.Models.Flights;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Services.Implementations
{
    public class FlightApiService : IFlightApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<FlightApiService> _logger;

        public FlightApiService(IHttpClientFactory httpClientFactory, ILogger<FlightApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<FlightReadDto>> GetFlightsByAirlineAsync(string airlineCode, int orgId, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                
                // Inyectar el Token JWT en los Headers para peticiones seguras (Prueba de Rúbrica)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var url = $"api/flights/airline/{airlineCode}?orgId={orgId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<FlightReadDto>>>();
                    return result?.Value ?? new List<FlightReadDto>();
                }
                
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning($"Failed to fetch flights. Status Code: {response.StatusCode}. Content: {errorContent}. Token length: {token.Length}");
                throw new ApplicationException($"La API rechazó la conexión ({(int)response.StatusCode} {response.StatusCode}). Verifica que el puerto sea el correcto.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comunicándose con la API backend al obtener vuelos.");
                throw; // Relanzar la excepción para que el PageModel la capture y la muestre en pantalla.
            }
        }
    }

    // Clase auxiliar para mapear el OperationResult del Backend
    public class ApiResponse<T>
    {
        [System.Text.Json.Serialization.JsonPropertyName("value")]
        public T? Value { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("success")]
        public bool Success { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public string? Message { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("errorCode")]
        public string? ErrorCode { get; set; }
    }
}
