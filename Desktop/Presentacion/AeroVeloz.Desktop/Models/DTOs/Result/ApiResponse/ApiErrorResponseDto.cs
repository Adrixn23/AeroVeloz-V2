using System.Text.Json.Serialization;

namespace AeroVeloz.Desktop.Models.DTOs.Result.ApiResponse;

public class ValidationErrorDto
{
    [JsonPropertyName("code")]
    public string? Code { get; set; } 

    [JsonPropertyName("description")]
    public string? Description { get; set; } 
}

public class ApiErrorResponseDto
{
    public bool Success { get; set; }
    public string? Message { get; set; } 
    public string ErrorCode { get; set; } 
    public ValidationErrorDto[] ValidationErrors { get; set; } = [];
}
