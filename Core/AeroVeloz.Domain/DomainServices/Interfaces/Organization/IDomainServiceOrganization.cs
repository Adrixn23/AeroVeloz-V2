using AeroVeloz.Domain.Entities.Organization.Base;
using AeroVeloz.Domain.Entities.Users.User;

namespace AeroVeloz.Domain.DomainServices.Interfaces.Organization
{
    public interface IDomainServiceOrganization
    {
        Task<Organizations> ExistByOrgAsync(int orgId);
        Task<bool> ExistActiveAsync(int orgId);
        Task<bool> NameOrganizationExistAsync(int orgId);
        Task<bool> EmailOrganizationExistAsync(int orgId, string email);
       
    }
}
