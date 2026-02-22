using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities;

public partial class AuditEntry
{
    public Guid AuditId { get; set; }

    public string? ActionType { get; set; }

    public string? TableName { get; set; }

    public string? RecordId { get; set; }

    public DateTime? ChangeDate { get; set; }

    public string? Details { get; set; }

    public Guid? ActorRef { get; set; }
}
