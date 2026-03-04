using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class OperationalChangeType
{
    public short IdOperationalType { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<OperationChange> OperationChanges { get; set; } = new List<OperationChange>();
}
