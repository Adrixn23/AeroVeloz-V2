using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class Audit
{
    public Guid IdAuditEntry { get; set; }

    public short IdAuditType { get; set; }

    public Guid IdUser { get; set; }

    public string NameEntity { get; set; } = null!;

    public DateTime OcurrentAt { get; set; }

    public string DataOld { get; set; } = null!;

    public string DataNew { get; set; } = null!;

    public virtual AuditType IdAuditTypeNavigation { get; set; } = null!;
}
