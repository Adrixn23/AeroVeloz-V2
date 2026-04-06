using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.Models.Organization;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Organization
{

    public class OrganizationRepository : IDomainServiceOrganization
    {
        private readonly AeroVelozContext _context;
        private readonly ILogger<OrganizationRepository> _logger;

        public OrganizationRepository(AeroVelozContext context, ILogger<OrganizationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OrganizationModel?> GetByIdAsync(int orgId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando organización por ID: {Id}", orgId);
                return null;
            }
        }

        public async Task<bool> ExistActiveAsync(int orgId)
        {
            try
            {
                return await _context.Organizations
                    .AsNoTracking()
                    .AnyAsync(o => o.Id == orgId && o.isActived);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verificando si la organización está activa por ID: {Id}", orgId);
                return false;
            }
        }

        public async Task<bool> ExistsByIdAsync(int orgId)
        {
            try
            {
                return await _context.Organizations
                    .AsNoTracking()
                    .AnyAsync(o => o.Id == orgId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verificando existencia de organización por ID: {Id}", orgId);
                return false;
            }
        }

        public async Task<OrganizationModel?> GetByEmailAsync(string email)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando organización por email: {Email}", email);
                return null;
            }
        }
    }
}
