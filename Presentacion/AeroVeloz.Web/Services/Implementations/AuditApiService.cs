using System.Net.Http.Headers;
using AeroVeloz.Web.Models.Audit;
using AeroVeloz.Web.Services.Implementations;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Services.Implementations
{
    public class AuditApiService : IAuditApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AuditApiService> _logger;

        public AuditApiService(IHttpClientFactory httpClientFactory, ILogger<AuditApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<AuditDetailDto>> GetOrganizationAuditsAsync(int orgId, string token, string userId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("userId", userId);

                var response = await client.GetAsync($"api/audits/organization/{orgId}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<AuditDetailDto>>>();
                    return result?.Value ?? new List<AuditDetailDto>();
                }

                _logger.LogWarning($"Audit fetch failed: {response.StatusCode}");
                return new List<AuditDetailDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching audits");
                return new List<AuditDetailDto>();
            }
        }

        public async Task<List<AuditDetailDto>> GetUserAuditsAsync(Guid targetUserId, string token, string userId, int orgId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("userId", userId);
                client.DefaultRequestHeaders.Add("orgId", orgId.ToString());

                var response = await client.GetAsync($"api/audits/user/{targetUserId}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<AuditDetailDto>>>();
                    return result?.Value ?? new List<AuditDetailDto>();
                }
                return new List<AuditDetailDto>();
            }
            catch { return new List<AuditDetailDto>(); }
        }
    }
}
