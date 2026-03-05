using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class ConectionsAirlineAirport
{
    public Guid IdConection { get; set; }

    public string CodeAirlines { get; set; } = null!;

    public string CodeAirport { get; set; } = null!;

    public string TokenApi { get; set; } = null!;

    public virtual Airline CodeAirlinesNavigation { get; set; } = null!;

    public virtual Airport CodeAirportNavigation { get; set; } = null!;

}
