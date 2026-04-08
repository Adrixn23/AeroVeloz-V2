using System.Text.Json.Serialization;

namespace AeroVeloz.Desktop.Models.DTOs;

public class LoginRequestDto
{
    [JsonPropertyName("emailOrganization")]
    public string? EmailOrganization { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; } 

    [JsonPropertyName("nameUser")]
    public string? NameUser { get; set; } 
}
