using AeroVeloz.Domain.DomainService.Interfaces.Organization;
using AeroVeloz.Domain.Models.Organization;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Organization
{
   
    public class OrganizationRepository : IDomainServiceOrganization
    {
        private readonly AeroVelozContext _context;

        public OrganizationRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<OrganizationModel?> GetByIdAsync(int orgId)
        {
            var org = await _context.Organizations
                .AsNoTracking()
                .Where(o => o.Id == orgId)
                .Select(o => new OrganizationModel(
                    o.Id,
                    o.nameOrganization,
                    o.typeOrganization,
                    o.isActived,
                    o.emailOrganization
                ))
                .FirstOrDefaultAsync();

            return org;
        }

        public async Task<bool> ExistActiveAsync(int orgId)
        {
            return await _context.Organizations
                .AsNoTracking()
                .AnyAsync(o => o.Id == orgId && o.isActived);
        }

        public async Task<bool> ExistsByIdAsync(int orgId)
        {
            return await _context.Organizations
                .AsNoTracking()
                .AnyAsync(o => o.Id == orgId);
        }

        public async Task<OrganizationModel?> GetByEmailAsync(string email)
        {
            var org = await _context.Organizations
                .AsNoTracking()
                .Where(o => o.emailOrganization == email)
                .Select(o => new OrganizationModel(
                    o.Id,
                    o.nameOrganization,
                    o.typeOrganization,
                    o.isActived,
                    o.emailOrganization
                ))
                .FirstOrDefaultAsync();

            return org;
        }
    }
}
