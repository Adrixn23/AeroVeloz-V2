using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities;

public partial class ViewPublicFlights48h
{
    public string FlightNumber { get; set; } = null!;

    public string AirlineName { get; set; } = null!;

    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public DateTime ScheduledTime { get; set; }

    public string StateName { get; set; } = null!;
}
