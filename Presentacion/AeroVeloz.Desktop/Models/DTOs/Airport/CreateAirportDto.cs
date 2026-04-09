using System.Text.Json.Serialization;

namespace AeroVeloz.Desktop.Models.DTOs.Airport;

public class CreateAirportDto
{
    [JsonPropertyName("nameOrganization")]
    public string? NameOrganization { get; set; }

    [JsonPropertyName("emailOrganization")]
    public string? EmailOrganization { get; set; } 

    [JsonPropertyName("codeICAO")]
    public string? CodeAirportIcao { get; set; } 

    [JsonPropertyName("codeIATA")]
    public string? CodeAirportIata { get; set; } 
    [JsonPropertyName("country")]
    public string? Country { get; set; } 

    [JsonPropertyName("city")]
    public string? City { get; set; } 

    [JsonPropertyName("timeOffset")]
    public DateTimeOffset TimeOffset { get; set; }
}
