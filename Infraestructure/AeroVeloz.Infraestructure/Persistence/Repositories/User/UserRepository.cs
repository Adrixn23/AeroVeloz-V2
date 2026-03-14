using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Domain.Models.Users;
using AeroVeloz.Domain.Models.UserSystem;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AeroVeloz.Domain.DomainServices.Interfaces.User;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository, IDomainServiceUser
    {

        private readonly AeroVelozContext _context;

        public UserRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEntity(Domain.Entities.Users.User.User entity)
        {
            _context.Users.Add(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteEntity(Domain.Entities.Users.User.User entity)
        {
            var result = await _context.Users.Where(us => us.Id == entity.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(u => u.isActive, false)
                );
                   
                return result > 0;
           
        }
        public async Task<UserSystemModel> GetByUserName(string nameUser, int orgId) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.nameUser == nameUser && u.idOrganization == orgId );

            if (user != null)
            {
               return new UserSystemModel(
                    user!.Id,
                    user.nameUser,
                    (bool)user.isActive!,
                    user.failedLoginAttempts ?? 0,
                    user.lockedUntil
                );

            }
            return null!;
        }

        public async Task<IReadOnlyCollection<UserDetailModel>> GetUserByOrganizationsId( int orgId)
        {

            var users = await (
                 from u in _context.Users.AsNoTracking()
                 join r in _context.Roles.AsNoTracking()
                 on u.idRol  equals r.Id
                 join o in _context.Organizations.AsNoTracking()
                 on  u.idOrganization equals o.Id
                 where u.idOrganization == orgId
                 select  new UserDetailModel(u.Id, u.nameUser, o.nameOrganization, o.typeOrganization, u.isActive, r.nameRol , u.createAt )
                
                ).ToListAsync();
            if (users.Any())
                return users;

            return Array.Empty<UserDetailModel>();  
              
        }


        public async Task<bool> UpdateEntity(Domain.Entities.Users.User.User entity)
        {
            var hasher = new PasswordHasher<Domain.Entities.Users.User.User>();
            string hash = hasher.HashPassword(null!, entity.passwordHash!);

            var result = await  _context.Users.Where(us => us.Id == entity.Id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(u => u.nameUser, entity.nameUser!)
                  .SetProperty(u => u.isActive, entity.isActive)
                  .SetProperty(u => u.passwordHash, hash )
                  .SetProperty(u => u.idOrganization, entity.idOrganization)
                  .SetProperty(u => u.idRol, entity.idRol)
                
                );
            return result > 0;
        }

        public async Task<bool> ExistActiveUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user!.isActive;
        }

        public async Task<bool> UserNameExistOrganization(string? userName, int orgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.nameUser == userName && u.idOrganization == orgId);
            return user != null;
        }
    }
}
