using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Audit;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Common.Exceptions;
using AeroVeloz.Domain.Models.Audit;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace AeroVeloz.Infraestructure.Persistence.Repositories.Audit
{

    public class AuditRepository : IAuditRepository
    {
        private readonly AeroVelozContext _context;
        private readonly ILogger<AuditRepository> _logger;

        public AuditRepository(AeroVelozContext context, ILogger<AuditRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Domain.Entities.Audit.Audit audit)
        {
            try
            {
                _context.Audits.Add(audit);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear registro de auditoría");
                throw new DatabaseOperationException("Error persistiendo registro de auditoría en la base de datos", ex);
            }
        }

        public async Task<IReadOnlyCollection<AuditDetailModel>> GetByOrganizationAsync(
            int orgId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo auditoría por organización {OrgId}", orgId);
                return Array.Empty<AuditDetailModel>();
            }
        }

        public async Task<IReadOnlyCollection<AuditDetailModel>> GetByUserAsync(
            Guid userId)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo auditoría por usuario {UserId}", userId);
                return Array.Empty<AuditDetailModel>();
            }
        }

     
        public async Task<ValidationResult> ValidateAuditEntryAsync(Guid userId, short auditTypeId, string? entityName)
        {
            var errors = new List<ErrosValidationResults>();

            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ValidateAuditEntryAsync (UserId: {UserId}, TypeId: {TypeId})", userId, auditTypeId);
                errors.Add(ErrosValidationResults.Create("SERVER_ERROR", "El servicio no se encuentra disponible momentáneamente. Por favor, inténtelo de nuevo más tarde."));
                return new ValidationResult().Failur(errors);
            }
        }
    }
}