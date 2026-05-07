namespace AeroVeloz.Desktop.Models.DTOs.User;

public class CreateUserDto
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public int IdOrganization { get; set; }
    public short IdRol { get; set; }
}
