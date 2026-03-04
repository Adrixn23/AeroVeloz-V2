using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Infraestructure.Persistence.Context;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository
    {

        private readonly AeroVelozDbContext _context; //depdency injection del db context que permitira agregar el
        //el usuario 

        public UserRepository(AeroVelozDbContext context) { 
            _context = context;
        }

        public Task<bool> CreateEntity(Domain.Entities.Users.User.User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(Domain.Entities.Users.User.User entity)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Users.User.User> ExistEmailSystem(string? email)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Users.User.User?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetUserExistsByUserName(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Users.User.User>> GetUsersByActive()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Users.User.User> GetUserWithOrganization(Guid useId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(Domain.Entities.Users.User.User entity)
        {
            throw new NotImplementedException();
        }
    }
}
