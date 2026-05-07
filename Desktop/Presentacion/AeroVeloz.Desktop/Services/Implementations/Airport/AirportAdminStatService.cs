using System.Net.Http;
using System.Net.Http.Json;
using AeroVeloz.Desktop.Models.DTOs.StatusSystem;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations.Airport;

public class AirportAdminStatService : IAirportAdminStatService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISessionService _sessionService;
    private readonly string _endpoint;

    public AirportAdminStatService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ISessionService sessionService)
    {
        _httpClientFactory = httpClientFactory;
        _sessionService = sessionService;
        _endpoint = configuration["ApiEndpoints:AirportStats"] ?? "api/stats/airport";
    }

    public async Task<AirportAdminStatsDto?> GetAirportStatsAsync()
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");

        try 
        {
            var url = $"{_endpoint}?orgId={_sessionService.OrgId}&userId={_sessionService.UserId}";
            return await client.GetFromJsonAsync<AirportAdminStatsDto>(url);
        }
        catch(HttpRequestException)
        {
            return null;
        }
    }
}

