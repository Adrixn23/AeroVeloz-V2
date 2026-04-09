using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop.Services.Implementations.Airport;

public class OperationService : IOperationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _endpoint;

    public OperationService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _endpoint = configuration["ApiEndpoints:Operations"] ?? "api/operations";
    }

    public async Task<IEnumerable<Models.DTOs.Operation.OperationDto>> GetAirportOperationsAsync()
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var response = await client.GetFromJsonAsync<Models.DTOs.Result.ApiResponse.ApiResponse<IEnumerable<Models.DTOs.Operation.OperationDto>>>($"{_endpoint}/airports/changes");
            return response?.Success == true && response.Value != null ? response.Value : new List<Models.DTOs.Operation.OperationDto>();
        }
        catch
        {
            return new List<Models.DTOs.Operation.OperationDto>();
        }
    }

    public async Task<dynamic?> GetOperationByIdAsync(Guid operationId)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            return await client.GetFromJsonAsync<dynamic>($"{_endpoint}/{operationId}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> CreateOperationAsync(dynamic operation)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(operation),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(_endpoint, jsonContent);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateOperationAsync(Guid operationId, dynamic operation)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(operation),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync($"{_endpoint}/{operationId}", jsonContent);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteOperationAsync(Guid operationId)
    {
        var client = _httpClientFactory.CreateClient("AeroVelozApi");
        try
        {
            var dto = new { Id = operationId };
            var request = new HttpRequestMessage(HttpMethod.Delete, _endpoint)
            {
                Content = new StringContent(JsonSerializer.Serialize(dto), System.Text.Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}

