
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

using AeroVeloz.Desktop.Models.DTOs.User;
using AuthUserDto = AeroVeloz.Desktop.Models.DTOs.Auth.UserDto;
using Microsoft.Extensions.Configuration;
using AeroVeloz.Desktop.Services.Interfaces.AdminSystem;
using AeroVeloz.Desktop.Services.Interfaces.Auth;

namespace AeroVeloz.Desktop.Services.Implementations.AdminSystem;


public class AdminManagerService : IAdminManagerService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISessionService _sessionService;
    private readonly string _baseEndpoint;

    public AdminManagerService(
        IHttpClientFactory httpClientFactory, 
        IConfiguration configuration, 
        ISessionService sessionService)
    {
        _httpClientFactory = httpClientFactory;
        _sessionService = sessionService;
        _baseEndpoint = configuration["ApiEndpoints:ManagerUsers"] ?? "api/ManagerUsers";
    }

   
    public async Task<IEnumerable<AuthUserDto>> GetAvailableAdminsAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            var endpoint = $"{_baseEndpoint}/organization/{_sessionService.OrgId}?userId={_sessionService.UserId}";
            var response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                return new List<AuthUserDto>();
            }

            var content = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(content);
            var root = jsonDoc.RootElement;

          
            if (root.TryGetProperty("value", out var valueElement) && valueElement.ValueKind == JsonValueKind.Array)
            {
                var users = new List<AuthUserDto>();

                foreach (var userElement in valueElement.EnumerateArray())
                {
                    try
                    {
                        var idUser = userElement.TryGetProperty("idUser", out var idProp) 
                            ? idProp.GetString() ?? string.Empty 
                            : string.Empty;
                        var userName = userElement.TryGetProperty("userName", out var nameProp) 
                            ? nameProp.GetString() ?? string.Empty 
                            : string.Empty;
                        var nameRol = userElement.TryGetProperty("nameRol", out var rolProp) 
                            ? rolProp.GetString() ?? string.Empty 
                            : string.Empty;

                        var isActive = userElement.TryGetProperty("isActive", out var isActiveProp) && isActiveProp.ValueKind == System.Text.Json.JsonValueKind.True;
                        var isBlocked = userElement.TryGetProperty("isBlocked", out var isBlockedProp) && isBlockedProp.ValueKind == System.Text.Json.JsonValueKind.True;

                        users.Add(new AuthUserDto
                        {
                            Id = idUser,
                            FullName = userName,
                            Email = string.Empty,
                            Role = nameRol,
                            IsActive = isActive,
                            IsBlocked = isBlocked
                        });
                    }
                    catch
                    {
                        continue;
                    }
                }

                return users;
            }

            return new List<AuthUserDto>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en GetAvailableAdminsAsync: {ex.Message}");
            return new List<AuthUserDto>();
        }
    }


    public async Task<bool> CreateUserAsync(CreateUserDto dto)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            var endpoint = $"{_baseEndpoint}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";

            var requestDto = new
            {
                userName = dto.UserName,
                password = dto.Password,
                idOrganization = _sessionService.OrgId,
                idRol = dto.IdRol
            };

            var response = await client.PostAsJsonAsync(endpoint, requestDto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en CreateUserAsync: {ex.Message}");
            return false;
        }
    }

    
    public async Task<bool> UpdateUserAsync(EditUserDto dto)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            var endpoint = $"{_baseEndpoint}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";

            var requestDto = new
            {
                idUser = dto.IdUser,
                nameUser = dto.NameUser,
                password = dto.Password,
                isActive = true,
                idRol = dto.IdRol,
                idOrganization = _sessionService.OrgId
            };

            var response = await client.PutAsJsonAsync(endpoint, requestDto);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en UpdateUserAsync: {ex.Message}");
            return false;
        }
    }

   
    public async Task<bool> DeactivateUserAsync(Guid userId)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            var endpoint = $"{_baseEndpoint}/{userId}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";

            var response = await client.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en DeactivateUserAsync: {ex.Message}");
            return false;
        }
    }
}
