namespace AeroVeloz.Desktop.Models.DTOs;

public class ApiErrorResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = string.Empty;
    public object[] ValidationErrors { get; set; } = [];
}
