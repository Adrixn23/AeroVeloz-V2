using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Entities.Security;
using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities.Operations;

public partial class OperationChange
{
    public Guid OperationId { get; set; }

    public string? TypeChange { get; set; }

    public string? PreviousValue { get; set; }

    public string? NewValue { get; set; }

    public DateTime? ChangeAt { get; set; }

    public string? Cause { get; set; }

    public Guid FlightId { get; set; }

    public Guid ActorRef { get; set; }

    public virtual User ActorRefNavigation { get; set; } = null!;

    public virtual Flight Flight { get; set; } = null!;
}
