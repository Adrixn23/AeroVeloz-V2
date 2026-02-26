using AeroVeloz.Domain.Entities.BaseEntity;
using AeroVeloz.Domain.Common.Enums.auditoria;

namespace AeroVeloz.Domain.Entities.Audits;

public partial class Audits : BEntity<Guid>
{
    public AuditType AuditType { get; private set; }
    public Guid idUser { get; private set; }
    public string nameEntity { get; private set; }
    public DateTime occurentAt { get; private set; }
    public string? oldValues { get; private set; }
    public string? newValues { get; private set; }
    public string? additionalInfo { get; private set; }

    private Audits(Guid id, AuditType AuditType, Guid idUser, string nameEntity,
                 string? oldValues = null, string? newValues = null, string? additionalInfo = null)
    {
        this.Id = id;
        this.AuditType = AuditType;
        this.idUser = idUser;
        this.nameEntity = nameEntity;
        this.occurentAt = DateTime.UtcNow;
        this.oldValues = oldValues;
        this.newValues = newValues;
        this.additionalInfo = additionalInfo;
    }

    public static Audits CreateAuditLog(AuditType auditType, Guid userId, string entity,
                                      string? oldValues = null, string? newValues = null,
                                      string? additionalInfo = null)
    {
        return new Audits(Guid.NewGuid(), auditType, userId, entity, oldValues, newValues, additionalInfo);
    }
}
