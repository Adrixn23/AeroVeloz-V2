using System.Net.Http.Headers;
using AeroVeloz.Web.Models.Users;
using AeroVeloz.Web.Services.Implementations;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Services.Implementations
{
    public class UserApiService : IUserApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<UserStaffDto>> GetStaffByOrgAsync(int orgId, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var response = await client.GetAsync($"api/users/organization/{orgId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<UserStaffDto>>>();
                    return result?.Value ?? new List<UserStaffDto>();
                }
                return new List<UserStaffDto>();
            }
            catch { return new List<UserStaffDto>(); }
        }

        public async Task<bool> CreateStaffAsync(CreateStaffDto dto, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AeroVelozApi");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var response = await client.PostAsJsonAsync("api/users/staff", dto);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}
