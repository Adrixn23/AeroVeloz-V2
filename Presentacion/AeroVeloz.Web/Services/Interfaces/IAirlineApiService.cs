using AeroVeloz.Web.Models.Airlines;

namespace AeroVeloz.Web.Services.Interfaces
{
    public interface IAirlineApiService
    {
        Task<List<AirlineReadDto>> GetAllAirlinesAsync(string token);
        Task<AirlineReadDto?> GetAirlineByCodeAsync(string codeIcao, string token);
        Task<bool> CreateAirlineAsync(AirlineSaveDto dto, string token, string userId, int orgId);
        Task<bool> UpdateAirlineAsync(string codeIcao, AirlineSaveDto dto, string token, string userId, int orgId);
        Task<bool> DeleteAirlineAsync(string codeIcao, string token, string userId, int orgId);
    }
}
