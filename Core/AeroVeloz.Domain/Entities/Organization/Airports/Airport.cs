<<<<<<< HEAD
﻿using AeroVeloz.Domain.Entities.Organization.Base;
=======
﻿using AeroVeloz.Domain.Entities.BaseEntity;
using AeroVeloz.Domain.Entities.Organization.type;
>>>>>>> 122bf176a5ed04e6f77387ce809b47f1237f8f65

namespace AeroVeloz.Domain.Entities.Organization.Airports;

<<<<<<< HEAD
public partial class Airport : Organizations 
{
    public string? codeAirportIATA { get; init; }
    public string? codeAirportICAO { get; init; }
    public string? city { get; init; }
    public string? country { get; init; }
    public string? ApiKeyMaster { get; init; }
    public TimeZoneInfo? timeZone { get; init; }

=======
public partial class Airport : organization
{
  
    public string codeAiprot { get; private set; }
    public string codeAiportIATA { get; private set; }
    public string nameAirport { get; private set; }
    public string city { get; private set; }
    public string country { get; private set; }
    public string apiKeyMaster { get; private set; }
    public TimeZoneInfo timeZone { get; private set; }

   public Airport(int idOrganization, TypeOrganization typeOrganization, string? emailOrganization,
      string codeAiport, string codeAiportIATA, string nameAirport, string city,
       string country, string apiKeyMaster, TimeZoneInfo timeZone ) :

       base(idOrganization, typeOrganization, emailOrganization)
    {
        this.codeAiprot = codeAiport;
        this.codeAiportIATA = codeAiportIATA;
        this.nameAirport = nameAirport;
        this.city = city;
        this.country = country;
        this.apiKeyMaster = apiKeyMaster;
        this.timeZone = timeZone;
    }

    public static Airport CreateAirport(int idOrganization, TypeOrganization typeOrganization, 
        string? emailOrganization, string codeAiport, string codeAiportIATA, string nameAirport, string city,
        string country, string apiKeyMaster, TimeZoneInfo timeZone)
    {
        return new Airport(idOrganization, typeOrganization, emailOrganization, codeAiport,
            codeAiportIATA, nameAirport, city, country, apiKeyMaster,timeZone);
    }


   
>>>>>>> 122bf176a5ed04e6f77387ce809b47f1237f8f65
}
