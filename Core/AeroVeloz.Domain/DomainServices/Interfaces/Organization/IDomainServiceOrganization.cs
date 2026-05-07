using AeroVeloz.Domain.Models.Organization;
using System.Collections.Generic;

namespace AeroVeloz.Domain.DomainServices.Interfaces.Organization
{

    public interface IDomainServiceOrganization
    {

        Task<OrganizationModel?> GetByIdAsync(int orgId);


        Task<bool> ExistActiveAsync(int orgId);


        Task<bool> ExistsByIdAsync(int orgId);


        Task<OrganizationModel?> GetByEmailAsync(string email);

        Task<IEnumerable<OrganizationModel>> GetByTypeAsync(string type);

        Task<bool> UpdateOrganizationStatusAsync(int orgId, bool isActived);
    }
}
