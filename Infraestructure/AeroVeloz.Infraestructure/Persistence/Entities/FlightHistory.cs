using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class FlightHistory
{
    public short FlightNumber { get; set; }

    public string CodeAirlines { get; set; } = null!;

    public DateTime ChangeAt { get; set; }

    public string? Reason { get; set; }

    public byte FlightStatesIdAfter { get; set; }

    public byte FlightStatedsIdBefore { get; set; }

    public virtual Airline CodeAirlinesNavigation { get; set; } = null!;

    public virtual Flight FlightNumberNavigation { get; set; } = null!;

    public virtual FlightState FlightStatedsIdBeforeNavigation { get; set; } = null!;

    public virtual FlightState FlightStatesIdAfterNavigation { get; set; } = null!;
}
