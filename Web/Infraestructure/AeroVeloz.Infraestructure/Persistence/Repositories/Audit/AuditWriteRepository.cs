using AeroVeloz.Application.Repositories.Audit;
using AeroVeloz.Infraestructure.Persistence.context;
using AuditEntity = AeroVeloz.Domain.Entities.Audit.Audit;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Audit
{
    public class AuditWriteRepository : IAuditWriteRepository
    {
        private readonly AeroVelozContext _context;

        public AuditWriteRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterAuditAsync(AuditEntity audit)
        {
            _context.Audits.Add(audit);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
