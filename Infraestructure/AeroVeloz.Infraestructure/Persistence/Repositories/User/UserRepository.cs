using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Domain.Models.Users;
using AeroVeloz.Domain.Models.UserSystem;
using AeroVeloz.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AeroVeloz.Domain.DomainServices.Interfaces.User;
using AeroVeloz.Domain.Common.Enums;

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
            var hasher = new PasswordHasher<Object>();
            string hash = hasher.HashPassword(null!, entity.passwordHash);
            var user = new AeroVeloz.Infraestructure.Persistence.Entities.User
            {
                IdUser = entity.Id,
                NameUser = entity.nameUser!,
                PasswordHash = hash,
                IpAdress = entity.ipAdress,
                IdOrganization = entity.idOrganization,
                IdRol = entity.idRol
            };
            _context.Add(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteEntity(Domain.Entities.Users.User.User entity)
        {
            var result = await _context.Users.Where(us => us.IdUser == entity.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(u => u.IsActive, false)
                );
                   
                return result > 0;
           
        }

        public async Task<UserSystemModel> GetByUserName(string nameUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NameUser == nameUser);
 
            if (user == null)
            {
               return new UserSystemModel(
                    user!.IdUser,
                    user.NameUser,
                    (bool)user.IsActive!
                );
                   
            }
            return null!;
        }

        public async Task<IReadOnlyCollection<UserDetailModel>> GetUserByOrganizationsId(Guid userId, int orgId)
        {
            var organization = await _context.Organizations.FirstOrDefaultAsync(org => org.IdOrganizations == orgId);
            if (organization == null)
            {
                return Array.Empty<UserDetailModel>();
            }
            var users = await _context
               .Users
               .Where(u => u.IdUser == userId && u.IdOrganization == orgId)
               .Select(u => new UserDetailModel(
                   u.IdUser,
                   u.NameUser,
                   organization.NameOrganization,
                   Enum.Parse<OrganizationType>(organization.TypeOrganization),
                   u.IsActive ?? false,
                   new Domain.Entities.Users.Roles.Roles
                   {
                       Id = u.IdRol,
                       nameRol = u.IdRolNavigation.NameRol
                   },
                   u.IdRolNavigation.RolPermissions.Select(rp => new Domain.Entities.Users.Permission.Permission {
                      Id = rp.IdPermissionNavigation.IdPermission,
                      codePermision = rp.IdPermissionNavigation.CodePermission,
                      description = rp.IdPermissionNavigation.Description
                       }
                       ).ToList(), 
                   u.CreateAt ?? DateTime.MinValue
               ))
               .ToListAsync();
            return users;
        }


        public async Task<bool> UpdateEntity(Domain.Entities.Users.User.User entity)
        {
            var hasher = new PasswordHasher<Object>();
            string hash = hasher.HashPassword(null!, entity.passwordHash);

            var result = await  _context.Users.Where(us => us.IdUser == entity.Id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(u => u.NameUser, entity.nameUser!)
                  .SetProperty(u => u.IsActive, entity.isActive)
                  .SetProperty(u => u.PasswordHash, hash )
                  .SetProperty(u => u.IdOrganization, entity.idOrganization)
                  .SetProperty(u => u.IdRol, entity.idRol)
                
                );
            return result > 0;
        }

        public async Task<bool> ExistActiveUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == userId);
            return user?.IsActive ?? false;
        }

        public async Task<bool> UserNameExistOrganization(string? userName, int orgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NameUser == userName && u.IdOrganization == orgId);
            return user != null;
        }
    }
}
