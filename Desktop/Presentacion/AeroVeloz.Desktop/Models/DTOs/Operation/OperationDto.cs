using System.Text.Json.Serialization;

namespace AeroVeloz.Desktop.Models.DTOs.Operation;

public class OperationDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("idOperationalType")]
    public short IdOperationalType { get; set; }

    [JsonPropertyName("flightNumber")]
    public short FlightNumber { get; set; }

    [JsonPropertyName("codeAirlinesIcao")]
    public string? CodeAirline { get; set; }

    [JsonPropertyName("codeAirportIcao")]
    public string? CodeAirport { get; set; }

    [JsonPropertyName("previosValue")]
    public string? PreviousValue { get; set; }

    [JsonPropertyName("newValue")]
    public string? NewValue { get; set; }

    [JsonPropertyName("cause")]
    public string? Cause { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
}
