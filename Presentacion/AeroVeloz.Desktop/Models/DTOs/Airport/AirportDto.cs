namespace AeroVeloz.Desktop.Models.DTOs.Airport;

public class AirportDto
{
    public int Id { get; set; }
    public string? NameOrganization { get; set; } 
    public string? TypeOrganization { get; set; }
    public bool IsActived { get; set; }
    public string? EmailOrganization { get; set; } 
    public DateTime CreateAt { get; set; }
    public string? CodeAirportIcao { get; set; } 
    public string? CodeAirportIata { get; set; } 
    public string? Country { get; set; } 
    public string? City { get; set; }
    public string? ApiKeyMaster { get; set; }
    public DateTimeOffset TimeOffset { get; set; }
}
