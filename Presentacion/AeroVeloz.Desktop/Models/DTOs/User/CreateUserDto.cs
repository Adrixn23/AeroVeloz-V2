namespace AeroVeloz.Desktop.Models.DTOs.User;

/// <summary>
/// DTO para crear un nuevo usuario desde la presentación.
/// </summary>
public class CreateUserDto
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public short IdRol { get; set; }
}
