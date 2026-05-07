using AeroVeloz.Application.DTOs.Organization;
using AeroVeloz.Application.Handlers.Result;

namespace AeroVeloz.Application.Contracts.Organization
{
    public interface IOrganizationService
    {
        Task<OperationResult<IEnumerable<OrganizationDto>>> GetOrganizationsByTypeAsync(string type);
        Task<OperationResult<bool>> BlockOrganizationAsync(int orgId);
    }
}