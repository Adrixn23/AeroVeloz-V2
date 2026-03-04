using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.DomainServices.Interfaces.User
{
    public interface IDomainServiceUser
    {
        Task<bool> ExistActiveUserAsync(Guid userId);
        Task<bool> UserNameExistOrganization(Guid userId, int orgId);
        
    }
}
