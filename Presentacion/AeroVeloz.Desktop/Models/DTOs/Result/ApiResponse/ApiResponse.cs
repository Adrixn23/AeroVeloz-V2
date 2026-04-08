namespace AeroVeloz.Desktop.Models.DTOs.Result.ApiResponse;

public class ApiResponse<T>
{
    public T? Value { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
}
