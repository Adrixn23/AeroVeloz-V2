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
        public async Task<List<FlightReadDto>> GetFlightsByAirportAsync(string airportCode, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var url = $"api/flights/airport/{airportCode}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<FlightReadDto>>>();
                    return result?.Value ?? new List<FlightReadDto>();
                }
                
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning($"Failed to fetch flights by airport. Status Code: {response.StatusCode}. Content: {errorContent}");
                throw new ApplicationException($"La API rechazó la conexión ({(int)response.StatusCode} {response.StatusCode}).");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comunicándose con la API backend al obtener vuelos por aeropuerto.");
                throw;
            }
        }

        public async Task<List<FlightReadDto>> GetPublicFlightsAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                var url = $"api/flights/public";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<FlightReadDto>>>();
                    return result?.Value ?? new List<FlightReadDto>();
                }
                
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning($"Failed to fetch public flights. Status Code: {response.StatusCode}. Content: {errorContent}");
                throw new ApplicationException($"Error al obtener vuelos públicos ({(int)response.StatusCode}).");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comunicándose con la API backend al obtener vuelos públicos.");
                throw;
            }
        }

        public async Task<FlightReadDto?> GetFlightDetailAsync(short flightNumber, string airlineCode, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var url = $"api/flights/{flightNumber}/{airlineCode}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<FlightReadDto>>();
                    return result?.Value;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching details for flight {flightNumber}");
                return null;
            }
        }

        public async Task<bool> UpdateFlightStateAsync(FlightUpdateStateDto dto, string userId, int orgId, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var url = $"api/flights/state?userId={userId}&orgId={orgId}";
                var response = await client.PutAsJsonAsync(url, dto);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                
                var errorContent = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
                string detail = "";
                if (errorContent?.ValidationErrors != null && errorContent.ValidationErrors.Any())
                {
                    detail = " - " + string.Join(", ", errorContent.ValidationErrors.Select(e => e.Description));
                }
                throw new ApplicationException((errorContent?.Message ?? "No se pudo actualizar el estado del vuelo") + detail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comunicándose con la API backend al actualizar el vuelo.");
                throw;
            }
        }

        public async Task<bool> UploadBatchAsync(List<FlightBatchItemDto> batch, string userId, int orgId, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var url = $"api/flights/batch?userId={userId}&orgId={orgId}";
                var response = await client.PostAsJsonAsync(url, batch);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Error en carga masiva: {error}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en UploadBatchAsync");
                throw;
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

        [System.Text.Json.Serialization.JsonPropertyName("validationErrors")]
        public List<ApiValidationError>? ValidationErrors { get; set; }
    }

    public class ApiValidationError
    {
        [System.Text.Json.Serialization.JsonPropertyName("code")]
        public string? Code { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
