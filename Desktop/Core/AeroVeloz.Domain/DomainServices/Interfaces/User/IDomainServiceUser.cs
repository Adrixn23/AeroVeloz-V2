namespace AeroVeloz.Domain.DomainServices.Interfaces.User
{
    public interface IDomainServiceUser
    {
        Task<bool> ExistActiveUserAsync(Guid userId);
        Task<bool> UserNameExistOrganization(string? userName, int orgId);
        
    }
}
