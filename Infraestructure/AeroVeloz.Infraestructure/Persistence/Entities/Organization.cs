using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class Organization
{
    public int IdOrganizations { get; set; }

    public string TypeOrganization { get; set; } = null!;

    public bool? IsActive { get; set; }

    public string EmailOrganizations { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public string NameOrganization { get; set; } = null!;

    public virtual ICollection<Airline> Airlines { get; set; } = new List<Airline>();

    public virtual ICollection<Airport> Airports { get; set; } = new List<Airport>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
