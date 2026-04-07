namespace AeroVeloz.Desktop.Models.DTOs;

public class LoginRequestDto
{
    public string EmailOrganization { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NameUser { get; set; } = string.Empty;
}
