using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Models.Users;
using AeroVeloz.Domain.Models.UserSystem;

namespace AeroVeloz.Application.Repositories.Users
{
    public interface IUserRepository : IBRepository<User, Guid>
    {
        Task<IReadOnlyCollection<UserDetailModel>> GetUserByOrganizationsId(Guid userId, int orgId);
        Task<UserSystemModel> GetByUserName(string nameUser);

    }
}
