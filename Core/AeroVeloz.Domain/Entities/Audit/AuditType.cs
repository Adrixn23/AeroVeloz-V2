using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Audit
{
    public partial class AuditType : BEntity<short>
    {
        public string? nameAudit { get; init; }
    }
}
