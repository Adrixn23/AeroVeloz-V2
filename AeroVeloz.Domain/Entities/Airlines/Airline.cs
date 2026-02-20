using System;
using System.Collections.Generic;
using AeroVeloz.Domain.Entities.Flights;

namespace AeroVeloz.Domain.Entities.Airlines;

public partial class Airline
{
    public Guid AirlineId { get; set; }

    public string AirlineName { get; set; } = null!;

    public string AirlineCode { get; set; } = null!;

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
