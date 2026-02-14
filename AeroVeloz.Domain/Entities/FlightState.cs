using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities;

public partial class FlightState
{
    public Guid StateId { get; set; }

    public string StateName { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
