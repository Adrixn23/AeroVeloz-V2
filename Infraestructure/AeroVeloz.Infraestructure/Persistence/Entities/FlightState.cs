using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class FlightState
{
    public byte FlightStatesId { get; set; }

    public string CodeFlightState { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<FlightHistory> FlightHistoryFlightStatedsIdBeforeNavigations { get; set; } = new List<FlightHistory>();

    public virtual ICollection<FlightHistory> FlightHistoryFlightStatesIdAfterNavigations { get; set; } = new List<FlightHistory>();

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
