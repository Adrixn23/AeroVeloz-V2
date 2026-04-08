namespace AeroVeloz.Desktop.Models.DTOs.Airport;

public class AirportDto
{
    public int Id { get; set; }
    public string NameOrganization { get; set; } = string.Empty;
    public string TypeOrganization { get; set; } = string.Empty;
    public bool IsActived { get; set; }
    public string EmailOrganization { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public string CodeAirportIcao { get; set; } = string.Empty;
    public string CodeAirportIata { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ApiKeyMaster { get; set; } = string.Empty;
    public DateTimeOffset TimeOffset { get; set; }
}
