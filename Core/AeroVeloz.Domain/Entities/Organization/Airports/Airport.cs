using AeroVeloz.Domain.Entities.Organization.Base;

namespace AeroVeloz.Domain.Entities.Organization.Airports;

public partial class Airport : Organizations 
{
    public string? codeAirportIATA { get; init; }
    public string? codeAirportICAO { get; init; }
    public string? city { get; init; }
    public string? country { get; init; }
    public string? ApiKeyMaster { get; init; }
    public TimeZoneInfo? timeZone { get; init; }

}
