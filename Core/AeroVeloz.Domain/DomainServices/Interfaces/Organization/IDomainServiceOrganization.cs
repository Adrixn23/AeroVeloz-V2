using AeroVeloz.Domain.Models.Organization;

namespace AeroVeloz.Domain.DomainServices.Interfaces.Organization
{
   
    public interface IDomainServiceOrganization
    {
       
        Task<OrganizationModel?> GetByIdAsync(int orgId);

      
        Task<bool> ExistActiveAsync(int orgId);

      
        Task<bool> ExistsByIdAsync(int orgId);

    
        Task<OrganizationModel?> GetByEmailAsync(string email);
    }
}
