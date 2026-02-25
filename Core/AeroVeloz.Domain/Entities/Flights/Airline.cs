using AeroVeloz.Domain.Entities.BaseEntity;
using AeroVeloz.Domain.Entities.Organization.type;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities.Flight;

public partial class Airline : organization
{
  


    public string codeAirlines { get; private set; }
    public string Name { get; private set; } = null!;


    public string codeIATA { get; private set; }

    public int idOrganization { get; private set; }

    public Airline(int idOrganization, TypeOrganization typeOrganization, string? emailOrganization,string codeAirlines, string name, string codeIATA, int idOrganizaion) : base(idOrganization, typeOrganization, emailOrganization)
    {
        this.codeAirlines = codeAirlines;
        this.Name = name;
        this.codeIATA = codeIATA;
        this.idOrganization = idOrganization;
    }

}
