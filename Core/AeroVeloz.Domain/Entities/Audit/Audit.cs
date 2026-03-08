using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Audits;

public partial class Audit : BEntity<Guid>
{
    public short IdAuditType { get;  init; }
    public Guid idUser { get;  init; }
    public string? nameEntity { get;  init; }
    public DateTime occurentAt { get;  init; }
    public string? oldValues { get;  init; }
    public string? DataOld { get; init; }
    public string? DataNew { get; init; }

}
