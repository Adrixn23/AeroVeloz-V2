using System.Net.Http;
using System.Net.Http.Json;
using AeroVeloz.Desktop.Models.DTOs.User;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using AeroVeloz.Desktop.Services.Interfaces.Users;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations.Users;

public class ManagerUserService : IManagerUserService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISessionService _sessionService;
    private readonly string _endpoint;

    public ManagerUserService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ISessionService sessionService)
    {
        _httpClientFactory = httpClientFactory;
        _sessionService = sessionService;
        _endpoint = configuration["ApiEndpoints:Users"] ?? "api/ManagerUsers";
    }

    public async Task<IEnumerable<UserDto>> GetAirportUsersAsync()
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var url = $"{_endpoint}/organization/{_sessionService.OrgId}?userId={_sessionService.UserId}";
            var response = await client.GetFromJsonAsync<Models.DTOs.Result.ApiResponse.ApiResponse<IEnumerable<UserDto>>>(url);
            return response?.Success == true && response.Value != null ? response.Value : new List<UserDto>();
        }
        catch
        {
            return new List<UserDto>();
        }
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            return await client.GetFromJsonAsync<UserDto>($"{_endpoint}/{userId}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> CreateUserAsync(CreateUserDto user)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var response = await client.PostAsJsonAsync($"{_endpoint}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}", user);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateUserAsync(Guid userId, EditUserDto user)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var response = await client.PutAsJsonAsync($"{_endpoint}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}", user);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var response = await client.DeleteAsync($"{_endpoint}/{userId}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}

