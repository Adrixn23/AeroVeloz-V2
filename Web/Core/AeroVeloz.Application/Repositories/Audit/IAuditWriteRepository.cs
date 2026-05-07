namespace AeroVeloz.Application.Repositories.Audit
{
    public interface IAuditWriteRepository
    {
        Task<bool> RegisterAuditAsync(Domain.Entities.Audit.Audit audit);
    }
}
