using AeroVeloz.Web.Models.Audit;

namespace AeroVeloz.Web.Services.Interfaces
{
    public interface IAuditApiService
    {
        Task<List<AuditDetailDto>> GetOrganizationAuditsAsync(int orgId, string token, string userId);
        Task<List<AuditDetailDto>> GetUserAuditsAsync(Guid targetUserId, string token, string userId, int orgId);
    }
}
