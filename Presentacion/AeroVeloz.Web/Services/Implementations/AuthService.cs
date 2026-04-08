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
                
                // Realizar peticion POST a la API de backend
                var response = await client.PostAsJsonAsync("api/auth/login", request);

                if (response.IsSuccessStatusCode)
                {
                    // Deserializar la respuesta exitosa
                    return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                }
                
                // Si la API responde con 400 Bad Request o 401 Unauthorized
                _logger.LogWarning($"Login failed with status code {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                // Manejo de Resiliencia: API caída o Timeouts
                _logger.LogError(ex, "Error comunicándose con la API backend durante el Login.");
                throw new ApplicationException("El servicio de autenticación no está disponible en este momento. Inténtelo más tarde.");
            }
        }
    }
}
