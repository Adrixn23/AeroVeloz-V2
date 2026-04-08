using System.Net.Http.Headers;
using AeroVeloz.Web.Models.Airlines;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Services.Implementations
{
    public class AirlineApiService : IAirlineApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AirlineApiService> _logger;

        public AirlineApiService(IHttpClientFactory httpClientFactory, ILogger<AirlineApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        private HttpClient CreateClient(string token, string? userId = null, int? orgId = null)
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (!string.IsNullOrEmpty(userId))
                client.DefaultRequestHeaders.Add("userId", userId);
            if (orgId.HasValue)
                client.DefaultRequestHeaders.Add("orgId", orgId.Value.ToString());
            return client;
        }

        public async Task<List<AirlineReadDto>> GetAllAirlinesAsync(string token)
        {
            try
            {
                var client = CreateClient(token);
                var response = await client.GetAsync("api/airlines");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<AirlineReadDto>>>();
                    return result?.Value ?? new List<AirlineReadDto>();
                }
                
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Error al obtener aerolíneas: {error}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAllAirlinesAsync");
                throw;
            }
        }

        public async Task<AirlineReadDto?> GetAirlineByCodeAsync(string codeIcao, string token)
        {
            try
            {
                var client = CreateClient(token);
                var response = await client.GetAsync($"api/airlines/{codeIcao}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<AirlineReadDto>>();
                    return result?.Value;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAirlineByCodeAsync");
                throw;
            }
        }

        public async Task<bool> CreateAirlineAsync(AirlineSaveDto dto, string token, string userId, int orgId)
        {
            try
            {
                var client = CreateClient(token, userId, orgId);
                var response = await client.PostAsJsonAsync("api/airlines", dto);
                
                if (response.IsSuccessStatusCode) return true;
                
                var error = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
                throw new ApplicationException(error?.Message ?? "Error al crear la aerolínea");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CreateAirlineAsync");
                throw;
            }
        }

        public async Task<bool> UpdateAirlineAsync(string codeIcao, AirlineSaveDto dto, string token, string userId, int orgId)
        {
            try
            {
                var client = CreateClient(token, userId, orgId);
                var response = await client.PutAsJsonAsync($"api/airlines/{codeIcao}", dto);
                
                if (response.IsSuccessStatusCode) return true;
                
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException("Error al actualizar la aerolínea");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en UpdateAirlineAsync");
                throw;
            }
        }

        public async Task<bool> DeleteAirlineAsync(string codeIcao, string token, string userId, int orgId)
        {
            try
            {
                var client = CreateClient(token, userId, orgId);
                var response = await client.DeleteAsync($"api/airlines/{codeIcao}");
                
                if (response.IsSuccessStatusCode) return true;
                
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException("Error al eliminar la aerolínea");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DeleteAirlineAsync");
                throw;
            }
        }
    }
}
