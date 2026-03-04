using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Users.User;

namespace AeroVeloz.Application.Repositories.Users
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<bool> GetUserExistsByUserName(Guid userId);
        Task<User> GetUserWithOrganization(Guid useId);
        Task<User> ExistEmailSystem(string? email);
        Task<IEnumerable<User>> GetUsersByActive();

    }
}
