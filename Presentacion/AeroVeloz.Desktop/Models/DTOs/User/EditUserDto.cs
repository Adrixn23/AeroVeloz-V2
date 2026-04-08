namespace AeroVeloz.Desktop.Models.DTOs.User;

/// <summary>
/// DTO para editar un usuario existente desde la presentación.
/// </summary>
public class EditUserDto
{
    public Guid IdUser { get; set; }
    public string? NameUser { get; set; }
    public string? Password { get; set; }
    public short IdRol { get; set; }
}
