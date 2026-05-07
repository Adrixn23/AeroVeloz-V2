using System.Net.Http;
using System.Net.Http.Json;
using AeroVeloz.Desktop.Models.DTOs.Flight;
using AeroVeloz.Desktop.Models.DTOs.Result.ApiResponse;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations.Airport;

public class FlightService : IFlightService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISessionService _sessionService;
    private readonly string _endpoint;

    public FlightService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ISessionService sessionService)
    {
        _httpClientFactory = httpClientFactory;
        _sessionService = sessionService;
        _endpoint = configuration["ApiEndpoints:Flights"] ?? "api/flights";
    }

    public async Task<IEnumerable<FlightForOperationDto>> GetFlightsForOperationsAsync()
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var response = await client.GetFromJsonAsync<ApiResponse<IEnumerable<FlightForOperationDto>>>($"{_endpoint}/active");
            return response?.Success == true && response.Value != null ? response.Value : new List<FlightForOperationDto>();
        }
        catch
        {
            return new List<FlightForOperationDto>();
        }
    }

    public async Task<IEnumerable<FlightForOperationDto>> GetFlightsByAirportAsync(string airportCode)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var response = await client.GetFromJsonAsync<ApiResponse<IEnumerable<FlightForOperationDto>>>($"{_endpoint}/airport/{airportCode}");
            return response?.Success == true && response.Value != null ? response.Value : new List<FlightForOperationDto>();
        }
        catch
        {
            return new List<FlightForOperationDto>();
        }
    }

    public async Task<FlightForOperationDto?> GetFlightDetailsAsync(short flightId)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var response = await client.GetFromJsonAsync<ApiResponse<FlightForOperationDto>>($"{_endpoint}/{flightId}");
            return response?.Success == true ? response.Value : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<FlightOperationDto>> GetFlightOperationsAsync(short flightId)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var response = await client.GetFromJsonAsync<ApiResponse<IEnumerable<FlightOperationDto>>>($"{_endpoint}/{flightId}/operations");
            return response?.Success == true && response.Value != null ? response.Value : new List<FlightOperationDto>();
        }
        catch
        {
            return new List<FlightOperationDto>();
        }
    }
}
