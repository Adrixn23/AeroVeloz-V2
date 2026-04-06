using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Audit;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Models.Audit;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
namespace AeroVeloz.Infraestructure.Persistence.Repositories.Audit
{

    public class AuditRepository : IAuditRepository
    {
        private readonly AeroVelozContext _context;

        public AuditRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Domain.Entities.Audit.Audit audit)
        {
            _context.Audits.Add(audit);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IReadOnlyCollection<AuditDetailModel>> GetByOrganizationAsync(
            int orgId)
        {
            var query = await (
                from a in _context.Audits.AsNoTracking()
                join at in _context.AuditTypes.AsNoTracking()
                    on a.IdAuditType equals at.idAuditType
                join u in _context.Users.AsNoTracking()
                    on a.idUser equals u.Id
                join o in _context.Organizations.AsNoTracking()
                    on u.idOrganization equals o.Id
                where u.idOrganization == orgId
                select
                new AuditDetailModel(
                    a.Id,
                    at.nameAudit,
                    a.idUser,
                    u.nameUser,
                    o.Id,
                    o.nameOrganization,
                    a.nameEntity,
                    a.ocurrentAt,
                    a.newValuesData
                )).ToListAsync();


            return query;
         
        }

        public async Task<IReadOnlyCollection<AuditDetailModel>> GetByUserAsync(
            Guid userId)
        {
            var query = await(
                from a in _context.Audits.AsNoTracking()
                join at in _context.AuditTypes.AsNoTracking()
                    on a.IdAuditType equals at.idAuditType
                join u in _context.Users.AsNoTracking()
                    on a.idUser equals u.Id
                join o in _context.Organizations.AsNoTracking()
                    on u.idOrganization equals o.Id
                where a.idUser == userId
                select new AuditDetailModel(
                    a.Id,
                    at.nameAudit,
                    a.idUser,
                    u.nameUser,
                    o.Id,
                    o.nameOrganization,
                    a.nameEntity,
                    a.ocurrentAt,
                    a.newValuesData
                ))
                .ToListAsync();
                    
                
             

            return query;
        }

        //public async Task<bool> ExistsAsync(Guid auditId)
        //{
        //    return await _context.Audits
        //        .AsNoTracking()
        //        .AnyAsync(a => a.Id == auditId);
        //}

        public async Task<ValidationResult> ValidateAuditEntryAsync(Guid userId, short auditTypeId, string? entityName)
        {
            var errors = new List<ErrosValidationResults>();

            if (userId == Guid.Empty)
                errors.Add(AuditErrors.InvalidUserId);

            var userExists = await _context.Users.AsNoTracking().AnyAsync(u => u.Id == userId);
            if (!userExists)
                errors.Add(AuditErrors.UserNotFoundForAudit);

            var typeExists = await _context.AuditTypes.AsNoTracking().AnyAsync(t => t.idAuditType == auditTypeId);
            if (!typeExists)
                errors.Add(AuditErrors.AuditTypeNotFound);

            if (string.IsNullOrWhiteSpace(entityName))
                errors.Add(AuditErrors.EntityNameRequired);
            else if (entityName.Length > 30)
                errors.Add(AuditErrors.MaxEntityNameLength);

            var result = new ValidationResult();
            return errors.Count > 0 ? result.Failur(errors) : result.Success();
        }
    }
}