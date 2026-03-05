using AeroVeloz.Domain.DomainServices.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using AeroVeloz.Infraestructure.Persistence.Context;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.User
{
    public class DomainServiceUser : IDomainServiceUser
    {

        private readonly AeroVelozContext _context;

        public DomainServiceUser(AeroVelozContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistActiveUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == userId);
            return user?.IsActive ?? false;
        }

        public async Task<bool> UserNameExistOrganization(string? userName, int orgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NameUser ==  userName && u.IdOrganization == orgId);
            return user != null;
        }
    }
}
