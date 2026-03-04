using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class Airline
{
    public string CodeAirlines { get; set; } = null!;

    public string CodeIata { get; set; } = null!;

    public int IdOrganization { get; set; }

    public virtual ICollection<ConectionsAirlineAirport> ConectionsAirlineAirports { get; set; } = new List<ConectionsAirlineAirport>();

    public virtual ICollection<FlightHistory> FlightHistories { get; set; } = new List<FlightHistory>();

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual Organization IdOrganizationNavigation { get; set; } = null!;

    public virtual ICollection<OperationChange> OperationChanges { get; set; } = new List<OperationChange>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
