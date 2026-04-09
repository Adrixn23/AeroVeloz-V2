namespace AeroVeloz.Desktop.Models.DTOs.User;

public class EditUserDto
{
    public Guid IdUser { get; set; }
    public string? NameUser { get; set; }
    public string? Password { get; set; }
    public bool IsActive { get; set; }
    public short IdRol { get; set; }
    public int IdOrganization { get; set; }
}
