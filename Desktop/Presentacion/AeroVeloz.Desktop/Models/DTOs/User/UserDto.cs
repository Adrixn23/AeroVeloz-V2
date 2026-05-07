using System.Text.Json.Serialization;

namespace AeroVeloz.Desktop.Models.DTOs.User;

public class UserDto
{
    [JsonPropertyName("idUser")]
    public string? Id { get; set; } 

    [JsonPropertyName("userName")]
    public string? UserName { get; set; }

    [JsonPropertyName("nameUser")]
    public string? FullName { get; set; }

    public string? Email { get; set; }

    [JsonPropertyName("nameRol")]
    public string? NameRol { get; set; }

    [JsonPropertyName("nameOrganization")]
    public string? NameOrganization { get; set; }

    [JsonPropertyName("organizationType")]
    public string? OrganizationType { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("isBlocked")]
    public bool IsBlocked { get; set; }
}
