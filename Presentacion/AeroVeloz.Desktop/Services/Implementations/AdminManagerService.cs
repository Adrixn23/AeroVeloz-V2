using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations;

public class AdminManagerService : IAdminManagerService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _endpointAvailable;
    private readonly string _endpointAssign;

    public AdminManagerService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _endpointAvailable = configuration["ApiEndpoints:AvailableAdmins"] ?? "api/admins/available";
        _endpointAssign = configuration["ApiEndpoints:AssignAdmin"] ?? "api/admins/assign";
    }

    public async Task<IEnumerable<UserDto>> GetAvailableAdminsAsync()
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        return await client.GetFromJsonAsync<IEnumerable<UserDto>>(_endpointAvailable) ?? new List<UserDto>();
    }

    public async Task<bool> AssignAdminToAirportAsync(AssignAdminDto assignAdminDto)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        var response = await client.PostAsJsonAsync(_endpointAssign, assignAdminDto);

        return response.IsSuccessStatusCode;
    }
}
