using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations.Airport;

public class AirportConnectionService : IAirportConnectionService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISessionService _sessionService;
    private readonly IAirportService _airportService;
    private readonly string _endpoint;

    public AirportConnectionService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ISessionService sessionService, IAirportService airportService)
    {
        _httpClientFactory = httpClientFactory;
        _sessionService = sessionService;
        _airportService = airportService;
        _endpoint = configuration["ApiEndpoints:Connections"] ?? "api/AirportConnection";
    }

    public async Task<IEnumerable<Models.DTOs.Connection.ConnectionDto>> GetAirportConnectionsAsync()
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var airport = await _airportService.GetByIdAsync(_sessionService.OrgId);
            if (airport == null || string.IsNullOrWhiteSpace(airport.CodeAirportIcao))
            {
                return new List<Models.DTOs.Connection.ConnectionDto>();
            }

            var url = $"{_endpoint}?codeAirportIcao={airport.CodeAirportIcao}&userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";
            var response = await client.GetFromJsonAsync<Models.DTOs.Result.ApiResponse.ApiResponse<IEnumerable<Models.DTOs.Connection.ConnectionDto>>>(url);

            if (response?.Success == true && response.Value != null)
            {
                return response.Value;
            }

            return new List<Models.DTOs.Connection.ConnectionDto>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en GetAirportConnectionsAsync: {ex.Message}\n{ex.StackTrace}");
            return new List<Models.DTOs.Connection.ConnectionDto>();
        }
    }

    public async Task<dynamic?> GetConnectionByIdAsync(Guid connectionId)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            return await client.GetFromJsonAsync<dynamic>($"{_endpoint}/{connectionId}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> CreateConnectionAsync(dynamic connection)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var url = $"{_endpoint}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(connection),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, jsonContent);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateConnectionAsync(Guid connectionId, dynamic connection)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(connection),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"{_endpoint}/{connectionId}", jsonContent);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteConnectionAsync(Guid connectionId)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var airport = await _airportService.GetByIdAsync(_sessionService.OrgId);
            if (airport == null || string.IsNullOrWhiteSpace(airport.CodeAirportIcao)) return false;

            var url = $"{_endpoint}/{connectionId}?airportIcao={airport.CodeAirportIcao}&userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";
            var response = await client.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}


