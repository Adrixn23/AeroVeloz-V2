using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.Models.DTOs.Auth;
using AeroVeloz.Desktop.Models.DTOs.Result;
using AeroVeloz.Desktop.Models.DTOs.Result.ApiResponse;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations.Auth;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _endpoint;

    public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _endpoint = configuration["ApiEndpoints:Login"] ?? "api/auth/login";
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            var response = await client.PostAsJsonAsync(_endpoint, request);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = await response.Content.ReadFromJsonAsync<OperationResult<UserLoginResultDto>>(options);

                if (result?.Value != null)
                {
                    return new LoginResponseDto 
                    { 
                        Success = true,
                        Token = result.Value.Token,
                        UserId = result.Value.UserId,
                        OrganizationId = result.Value.OrganizationId,
                        RoleName = result.Value.RoleName
                    };
                }

                return new LoginResponseDto { Success = true }; 
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var apiError = JsonSerializer.Deserialize<ApiErrorResponseDto>(errorContent, options);

                return new LoginResponseDto 
                { 
                    Success = false, 
                    ErrorMessage = !string.IsNullOrWhiteSpace(apiError?.Message) 
                        ? apiError.Message 
                        : "Ocurrió un error al procesar las credenciales." 
                };
            }
            catch (JsonException)
            {
                return new LoginResponseDto 
                { 
                    Success = false, 
                    ErrorMessage = "Credenciales incorrectas o usuario no encontrado." 
                };
            }
        }
        catch (HttpRequestException)
        {
            return new LoginResponseDto 
            { 
                Success = false, 
                ErrorMessage = "Error de conexión con el servidor. Revisa tu internet e inténtalo nuevamente." 
            };
        }
        catch (Exception)
        {
            return new LoginResponseDto 
            { 
                Success = false, 
                ErrorMessage = "Ocurrió un error inesperado al intentar iniciar sesión. Por favor, intenta de nuevo más tarde." 
            };
        }
    }
}
