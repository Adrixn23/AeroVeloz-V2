
using System.Text.Json.Serialization;

namespace AeroVeloz.Desktop.Models.DTOs.Airport;

public class CreateAirportDto
{
    [JsonPropertyName("nameOrganization")]
    public string NameOrganization { get; set; } = string.Empty;

    [JsonPropertyName("emailOrganization")]
    public string EmailOrganization { get; set; } = string.Empty;

    [JsonPropertyName("codeICAO")]
    public string CodeAirportIcao { get; set; } = string.Empty;

    [JsonPropertyName("codeIATA")]
    public string CodeAirportIata { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("timeOffset")]
    public DateTimeOffset TimeOffset { get; set; }
}
