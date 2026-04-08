using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AeroVeloz.Desktop.Models.DTOs.Airport;
using AeroVeloz.Desktop.Models.DTOs.Result.ApiResponse;
using AeroVeloz.Desktop.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations;

public class AirportService : IAirportService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISessionService _sessionService;
    private readonly string _endpoint;

    public AirportService(
        IHttpClientFactory httpClientFactory, 
        IConfiguration configuration,
        ISessionService sessionService)
    {
        _httpClientFactory = httpClientFactory;
        _sessionService = sessionService;
        _endpoint = configuration["ApiEndpoints:Airports"] ?? "api/Airport";
    }

    public async Task<IEnumerable<AirportDto>> GetAllAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");
            var url = BuildQueryUrl(_endpoint);

            var response = await client.GetFromJsonAsync<ApiResponse<IEnumerable<AirportDto>>>(url);

            if (response?.Success == true && response.Value != null)
            {
                return response.Value;
            }

            return new List<AirportDto>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en GetAllAsync: {ex.Message}");
            return new List<AirportDto>();
        }
    }

    public async Task<AirportDto?> GetByIdAsync(int id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");
            var url = BuildQueryUrl($"{_endpoint}/{id}");

            var response = await client.GetFromJsonAsync<ApiResponse<AirportDto>>(url);

            return response?.Success == true ? response.Value : null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en GetByIdAsync: {ex.Message}");
            return null;
        }
    }

    public async Task<AirportDto?> CreateAsync(CreateAirportDto createAirportDto)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");
            var url = BuildQueryUrl(_endpoint);

            var httpResponse = await client.PostAsJsonAsync(url, createAirportDto);

            if (httpResponse.IsSuccessStatusCode)
            {
                var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<AirportDto>>();
                return response?.Success == true ? response.Value : null;
            }

            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en CreateAsync: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> UpdateAsync(int id, AirportDto airportDto)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");
            var url = BuildQueryUrl($"{_endpoint}");

            var updatePayload = new 
            {
                idOrg = airportDto.Id,
                nameOrganization = airportDto.NameOrganization,
                emailOrganization = airportDto.EmailOrganization,
                codeICAO = airportDto.CodeAirportIcao,
                codeIATA = airportDto.CodeAirportIata,
                country = airportDto.Country,
                city = airportDto.City,
                timeOffset = airportDto.TimeOffset,
                isActived = airportDto.IsActived
            };

            var httpResponse = await client.PutAsJsonAsync(url, updatePayload);
            return httpResponse.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en UpdateAsync: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("AeroVelozApi");
            var url = BuildQueryUrl($"{_endpoint}/{id}");

            var httpResponse = await client.DeleteAsync(url);
            return httpResponse.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en DeleteAsync: {ex.Message}");
            return false;
        }
    }

  
    private string BuildQueryUrl(string baseUrl)
    {
        return $"{baseUrl}?userId={_sessionService.UserId}&orgId={_sessionService.OrgId}";
    }
}
