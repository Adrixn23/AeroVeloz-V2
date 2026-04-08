namespace AeroVeloz.Desktop.Models.DTOs;

public class OperationResult<T>
{
    public T? Value { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
}