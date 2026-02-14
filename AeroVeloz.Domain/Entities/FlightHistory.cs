using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities;
public partial class FlightHistory
{
    public Guid HistoryId { get; set; }

    public Guid FlightId { get; set; }

    public Guid? OldStateId { get; set; }

    public Guid? NewStateId { get; set; }

    public DateTime? ChangeDate { get; set; }

    public Guid? ChangeBy { get; set; }

    public virtual Flight Flight { get; set; } = null!;
}
