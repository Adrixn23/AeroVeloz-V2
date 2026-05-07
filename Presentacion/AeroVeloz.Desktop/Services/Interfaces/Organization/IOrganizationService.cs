using AeroVeloz.Desktop.Models.DTOs.Organization;

namespace AeroVeloz.Desktop.Services.Interfaces.Organization
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationDto>> GetOrganizationsByTypeAsync(string type);
        Task<bool> BlockOrganizationAsync(int orgId);
    }
}
