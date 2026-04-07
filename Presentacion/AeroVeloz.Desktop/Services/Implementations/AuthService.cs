using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.Services.Interfaces;

namespace AeroVeloz.Desktop.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            var response = await client.PostAsJsonAsync("api/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                return result ?? new LoginResponseDto { Success = true }; 
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return new LoginResponseDto 
            { 
                Success = false, 
                ErrorMessage = string.IsNullOrWhiteSpace(errorContent) ? "Credenciales incorrectas o usuario no encontrado." : errorContent 
            };
        }
        catch (HttpRequestException ex)
        {
            return new LoginResponseDto 
            { 
                Success = false, 
                ErrorMessage = $"Error de comunicación con el servidor. Revise su conexión ({ex.Message})." 
            };
        }
        catch (Exception ex)
        {
            return new LoginResponseDto 
            { 
                Success = false, 
                ErrorMessage = $"Ocurrió un error inesperado: {ex.Message}" 
            };
        }
    }
}
