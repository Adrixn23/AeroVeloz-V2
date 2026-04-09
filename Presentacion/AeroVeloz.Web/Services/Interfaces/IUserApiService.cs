using AeroVeloz.Web.Models.Users;

namespace AeroVeloz.Web.Services.Interfaces
{
    public interface IUserApiService
    {
        Task<List<UserStaffDto>> GetStaffByOrgAsync(int orgId, string token);
        Task<bool> CreateStaffAsync(CreateStaffDto dto, string token);
        Task<bool> UpdateUserAsync(Guid userId, CreateStaffDto dto, string token);
        Task<bool> DeleteUserAsync(Guid userId, string token);
    }
}
