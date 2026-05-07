using AeroVeloz.Web.Models.Auth;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IHttpClientFactory httpClientFactory, ILogger<AuthService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                
                var response = await client.PostAsJsonAsync("api/auth/login", request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                }
                
                try 
                {
                    var errorResult = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                    if (errorResult != null && !string.IsNullOrEmpty(errorResult.Message))
                    {
                        throw new ApplicationException(errorResult.Message);
                    }
                }
                catch { /* Ignorar error de parseo si no es un JSON válido */ }

                _logger.LogWarning($"Login failed with status code {response.StatusCode}");
                return null;
            }
            catch (ApplicationException) { throw; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comunicándose con la API backend durante el Login.");
                throw new ApplicationException("El servicio de autenticación no está disponible en este momento. Inténtelo más tarde.");
            }
        }
    }
}
