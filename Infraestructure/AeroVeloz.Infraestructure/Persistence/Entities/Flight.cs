using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class Flight
{
    public short FlightNumber { get; set; }

    public string CodeAirlines { get; set; } = null!;

    public byte FlightStatesId { get; set; }

    public string OriginAirport { get; set; } = null!;

    public string DestinationAirport { get; set; } = null!;

    public DateTimeOffset ScheduledDeparture { get; set; }

    public DateTimeOffset ScheduledArrival { get; set; }

    public string BordingGate { get; set; } = null!;

    public string? BoardingGateArrived { get; set; }

    public virtual Airline CodeAirlinesNavigation { get; set; } = null!;

    public virtual Airport DestinationAirportNavigation { get; set; } = null!;

    public virtual ICollection<FlightHistory> FlightHistories { get; set; } = new List<FlightHistory>();

    public virtual FlightState FlightStates { get; set; } = null!;

    public virtual ICollection<OperationChange> OperationChanges { get; set; } = new List<OperationChange>();

    public virtual Airport OriginAirportNavigation { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
