namespace AeroVeloz.Desktop.Models.DTOs;

public class LoginResponseDto
{
    public Guid UserId { get; set; }
    public int OrganizationId { get; set; }
    public string? Token { get; set; } 
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; } 
}
