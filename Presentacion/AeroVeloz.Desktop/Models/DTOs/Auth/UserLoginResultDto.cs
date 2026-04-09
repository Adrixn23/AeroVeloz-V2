namespace AeroVeloz.Desktop.Models.DTOs.Auth;

public class UserLoginResultDto
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public int OrganizationId { get; set; }
    public string? RoleName { get; set; }
    public string? Token { get; set; }
}