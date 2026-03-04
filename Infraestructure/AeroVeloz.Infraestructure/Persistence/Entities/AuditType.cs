using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class AuditType
{
    public short IdAuditType { get; set; }

    public string NameAudit { get; set; } = null!;

    public virtual ICollection<Audit> Audits { get; set; } = new List<Audit>();
}
