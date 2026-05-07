using System.Text.Json.Serialization;

namespace AeroVeloz.Desktop.Models.DTOs.Connection;


public class ConnectionDto
{
    [JsonPropertyName("connectionId")]
    public string? Id { get; set; }

    [JsonPropertyName("airlineCode")]
    public string? CodeAirlinesIcao { get; set; }

    [JsonPropertyName("airportCode")]
    public string? CodeAirportIcao { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("createAt")]
    public DateTime CreateAt { get; set; }

    [JsonPropertyName("airlineName")]
    public string? AirlineName { get; set; }
}
