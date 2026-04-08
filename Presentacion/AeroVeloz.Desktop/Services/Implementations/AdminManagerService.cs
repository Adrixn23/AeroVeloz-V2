using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.Auth;
using AeroVeloz.Desktop.Models.DTOs.User;
using AeroVeloz.Desktop.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations;

/// <summary>
/// Servicio para gestionar usuarios del sistema a nivel de presentación.
/// Consume el endpoint ManagerUsers del API que usa los servicios de application correspondientes.
/// Realiza operaciones CRUD: Create, Read, Update, Deactivate.
/// </summary>
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

    /// <summary>
    /// Obtiene la lista de usuarios de la organización del sistema.
    /// Mapea UserDetailModel (del API) a UserDto (presentación).
    /// </summary>
    public async Task<IEnumerable<UserDto>> GetAvailableAdminsAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            // GET /api/ManagerUsers/organization/{orgId}?userId={userId}
            var endpoint = $"{_baseEndpoint}/organization/{_sessionService.OrgId}?userId={_sessionService.UserId}";
            var response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                return new List<UserDto>();
            }

            var content = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(content);
            var root = jsonDoc.RootElement;

            // La respuesta es OperationResult<IReadOnlyCollection<UserDetailModel>>
            // Estructura: { "value": [...], "success": true, ... }
            if (root.TryGetProperty("value", out var valueElement) && valueElement.ValueKind == JsonValueKind.Array)
            {
                var users = new List<UserDto>();

                foreach (var userElement in valueElement.EnumerateArray())
                {
                    try
                    {
                        // Mapear UserDetailModel ? UserDto
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

                        users.Add(new UserDto
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
                        // Ignorar usuarios problemáticos
                        continue;
                    }
                }

                return users;
            }

            return new List<UserDto>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en GetAvailableAdminsAsync: {ex.Message}");
            return new List<UserDto>();
        }
    }

    /// <summary>
    /// Crea un nuevo usuario en el sistema.
    /// POST /api/ManagerUsers?userId={userId}&orgId={orgId}
    /// </summary>
    public async Task<bool> CreateUserAsync(CreateUserDto dto)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            var endpoint = $"{_baseEndpoint}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";

            // Convertir a formato esperado por el API (UserSaveDto)
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

    /// <summary>
    /// Edita un usuario existente.
    /// PUT /api/ManagerUsers?userId={userId}&orgId={orgId}
    /// </summary>
    public async Task<bool> UpdateUserAsync(EditUserDto dto)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");

            var endpoint = $"{_baseEndpoint}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";

            // Convertir a formato esperado por el API (UserUpdateDto)
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

    /// <summary>
    /// Desactiva un usuario del sistema.
    /// DELETE /api/ManagerUsers/{userId}?userId={requestingUserId}&orgId={orgId}
    /// </summary>
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
