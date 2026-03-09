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
            int orgId, DateTime? from = null, DateTime? to = null)
        {
            var query =
                from a in _context.Audits.AsNoTracking()
                join at in _context.AuditTypes.AsNoTracking()
                    on a.IdAuditType equals at.Id
                join u in _context.Users.AsNoTracking()
                    on a.idUser equals u.Id
                join o in _context.Organizations.AsNoTracking()
                    on u.idOrganization equals o.Id
                where u.idOrganization == orgId
                select new { a, at, u, o };

            if (from.HasValue)
                query = query.Where(x => x.a.occurentAt >= from.Value);
            if (to.HasValue)
                query = query.Where(x => x.a.occurentAt <= to.Value);

            var audits = await query
                .OrderByDescending(x => x.a.occurentAt)
                .Select(x => new AuditDetailModel(
                    x.a.Id,
                    x.at.nameAudit,
                    x.a.idUser,
                    x.u.nameUser,
                    x.o.Id,
                    x.o.nameOrganization,
                    x.a.nameEntity,
                    x.a.occurentAt,
                    x.a.DataOld,
                    x.a.DataNew
                ))
                .ToListAsync();

            return audits;
        }

        public async Task<IReadOnlyCollection<AuditDetailModel>> GetByUserAsync(
            Guid userId, DateTime? from = null, DateTime? to = null)
        {
            var query =
                from a in _context.Audits.AsNoTracking()
                join at in _context.AuditTypes.AsNoTracking()
                    on a.IdAuditType equals at.Id
                join u in _context.Users.AsNoTracking()
                    on a.idUser equals u.Id
                join o in _context.Organizations.AsNoTracking()
                    on u.idOrganization equals o.Id
                where a.idUser == userId
                select new { a, at, u, o };

            if (from.HasValue)
                query = query.Where(x => x.a.occurentAt >= from.Value);
            if (to.HasValue)
                query = query.Where(x => x.a.occurentAt <= to.Value);

            var audits = await query
                .OrderByDescending(x => x.a.occurentAt)
                .Select(x => new AuditDetailModel(
                    x.a.Id,
                    x.at.nameAudit,
                    x.a.idUser,
                    x.u.nameUser,
                    x.o.Id,
                    x.o.nameOrganization,
                    x.a.nameEntity,
                    x.a.occurentAt,
                    x.a.DataOld,
                    x.a.DataNew
                ))
                .ToListAsync();

            return audits;
        }

        public async Task<bool> ExistsAsync(Guid auditId)
        {
            return await _context.Audits
                .AsNoTracking()
                .AnyAsync(a => a.Id == auditId);
        }

        public async Task<ValidationResult> ValidateAuditEntryAsync(Guid userId, short auditTypeId, string? entityName)
        {
            var errors = new List<ErrosValidationResults>();

            if (userId == Guid.Empty)
                errors.Add(AuditErrors.InvalidUserId);

            var userExists = await _context.Users.AsNoTracking().AnyAsync(u => u.Id == userId);
            if (!userExists)
                errors.Add(AuditErrors.UserNotFoundForAudit);

            var typeExists = await _context.AuditTypes.AsNoTracking().AnyAsync(t => t.Id == auditTypeId);
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
