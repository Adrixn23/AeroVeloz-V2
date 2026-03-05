using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Models.Users;
using AeroVeloz.Domain.Models.UserSystem;
using AeroVeloz.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository
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
            var user = await _context.Users.FirstOrDefaultAsync(us => us.IdUser == entity.Id);
            user!.IsActive = false;
            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync();
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
            //var users = await _context
            // .Users
            //   .Where(u => u.IdUser == userId && u.IdOrganization == orgId).
            //   Select(u => new UserDetailModel
            //   {
            //       idUser = u.IdUser,
            //       userName = u.NameUser,
            //       nameOrganization = organization.NameOrganization,
            //       Enum.Parse<OrganizationType>(organization.TypeOrganization),


            //   }).
            //   ToListAsync();

            return null;
            
        }

        public async Task<Domain.Entities.Users.User.User?> GetByEntityAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(us => us.IdUser == id);
            //return new Domain.Entities.Users.User.User
            //{
            //    Id = user!.IdUser,


            return null;
           
        }

        public Task<bool> UpdateEntity(Domain.Entities.Users.User.User entity)
        {
            throw new NotImplementedException();
        }
    }
}
