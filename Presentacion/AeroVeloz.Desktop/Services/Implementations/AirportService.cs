using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs;
using AeroVeloz.Desktop.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations;

public class AirportService : IAirportService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _endpoint;

    public AirportService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _endpoint = configuration["ApiEndpoints:Airports"] ?? "api/airports";
    }

    public async Task<IEnumerable<AirportDto>> GetAllAsync()
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        return await client.GetFromJsonAsync<IEnumerable<AirportDto>>(_endpoint) ?? new List<AirportDto>();
    }

    public async Task<AirportDto?> GetByIdAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        return await client.GetFromJsonAsync<AirportDto>($"{_endpoint}/{id}");
    }

    public async Task<AirportDto?> CreateAsync(CreateAirportDto createAirportDto)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        var response = await client.PostAsJsonAsync(_endpoint, createAirportDto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AirportDto>();
        }

        
        return null;
    }

    public async Task<bool> UpdateAsync(int id, AirportDto airportDto)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        var response = await client.PutAsJsonAsync($"{_endpoint}/{id}", airportDto);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        var response = await client.DeleteAsync($"{_endpoint}/{id}");
        return response.IsSuccessStatusCode;
    }
}
