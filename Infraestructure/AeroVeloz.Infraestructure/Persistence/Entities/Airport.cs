using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class Airport
{
    public string CodeAirport { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string ApiKeyMaster { get; set; } = null!;

    public DateTimeOffset TimeZone { get; set; }

    public string CodeIata { get; set; } = null!;

    public int IdOrganization { get; set; }

    public virtual ICollection<ConectionsAirlineAirport> ConectionsAirlineAirports { get; set; } = new List<ConectionsAirlineAirport>();

    public virtual ICollection<Flight> FlightDestinationAirportNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightOriginAirportNavigations { get; set; } = new List<Flight>();

    public virtual Organization IdOrganizationNavigation { get; set; } = null!;

    
}
