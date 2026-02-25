using AeroVeloz.Domain.Entities.BaseEntity;
using AeroVeloz.Domain.Entities.Organization.type;

namespace AeroVeloz.Domain.Entities.Airports;

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


   
}
