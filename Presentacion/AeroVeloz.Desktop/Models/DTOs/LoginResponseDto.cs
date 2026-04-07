namespace AeroVeloz.Desktop.Models.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
