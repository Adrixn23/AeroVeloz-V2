using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class OperationChange
{
    public Guid OperationId { get; set; }

    public Guid IdUser { get; set; }

    public short IdOperationalType { get; set; }

    public short FlighNumber { get; set; }

    public string CodeAirline { get; set; } = null!;

    public string PrivousValue { get; set; } = null!;

    public string NewValue { get; set; } = null!;

    public DateTime ChangeAt { get; set; }

    public string Cause { get; set; } = null!;

    public virtual Airline CodeAirlineNavigation { get; set; } = null!;

    public virtual Flight FlighNumberNavigation { get; set; } = null!;

    public virtual OperationalChangeType IdOperationalTypeNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
