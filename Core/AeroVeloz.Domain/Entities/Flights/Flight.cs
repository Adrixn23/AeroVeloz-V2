using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Entities.Subscriptions;
using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities.Flights;

public partial class Flight
{
    public Guid FlightId { get; set; }

    public string FlightNumber { get; set; } = null!;

    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public DateTime ScheduledTime { get; set; }

    public Guid AirlineId { get; set; }

    public Guid StateId { get; set; }

    public virtual Airline Airline { get; set; } = null!;

    public virtual ICollection<FlightHistory> FlightHistories { get; set; } = new List<FlightHistory>();

    public virtual ICollection<OperationChange> OperationChanges { get; set; } = new List<OperationChange>();

    public virtual FlightState State { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
