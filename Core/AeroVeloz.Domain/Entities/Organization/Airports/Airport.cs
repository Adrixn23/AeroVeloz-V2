using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Airport;

public partial class Airport : BEntity<string>
{
    public string nameAirport { get; private set; }
    public string city { get; private set; }
    public string country { get; private set; }
    public string emailAirport { get; private set; }
    public string apiKeyMaster { get; private set; }
    public bool isActive { get; private set; }
    public DateTime createdAt { get; private set; }
    public TimeZoneInfo timeZone { get; private set; }

    private Airport(string codeAirport, string nameAirport, string city, string country,
                   string emailAirport, string apiKeyMaster)
    {
        this.Id = codeAirport; 
        this.nameAirport = nameAirport;
        this.city = city;
        this.country = country;
        this.emailAirport = emailAirport;
        this.apiKeyMaster = apiKeyMaster;
        this.isActive = true;
        this.createdAt = DateTime.UtcNow;
    }

    public static Airport CreateAirport(string codeAirport, string nameAirport, string city,
                                   string country, string emailAirport, string apiKeyMaster)
    {
        return new Airport(codeAirport, nameAirport, city, country, emailAirport, apiKeyMaster);
    }

    public void DeactivateAirport()
    {
        isActive = false;
    }
}
