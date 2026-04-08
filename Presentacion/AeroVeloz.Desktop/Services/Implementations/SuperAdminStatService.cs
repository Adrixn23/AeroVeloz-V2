using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.StatusSystem;
using AeroVeloz.Desktop.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations;

public class SuperAdminStatService : ISuperAdminStatService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _endpoint;

    public SuperAdminStatService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _endpoint = configuration["ApiEndpoints:GlobalStats"] ?? "api/stats/global";
    }

    public async Task<GlobalStatsDto?> GetGlobalStatsAsync()
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");

        try 
        {
            return await client.GetFromJsonAsync<GlobalStatsDto>(_endpoint);
        }
        catch(HttpRequestException)
        {
            // Logging can be added here
            return null;
        }
    }
}
