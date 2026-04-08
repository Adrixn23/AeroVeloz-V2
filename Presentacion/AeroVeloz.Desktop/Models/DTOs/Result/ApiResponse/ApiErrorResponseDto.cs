using System.Text.Json.Serialization;

namespace AeroVeloz.Desktop.Models.DTOs.Result.ApiResponse;

public class ValidationErrorDto
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}

public class ApiErrorResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = string.Empty;
    public ValidationErrorDto[] ValidationErrors { get; set; } = [];
}
