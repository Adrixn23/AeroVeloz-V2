using System.Text.Json.Serialization;

namespace AeroVeloz.Web.Models.Auth
{
    public class LoginResponseDto
    {
        [JsonPropertyName("user")]
        public UserDto? User { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("tokenType")]
        public string TokenType { get; set; } = string.Empty;

        [JsonPropertyName("expiresAtUtc")]
        public DateTime ExpiresAtUtc { get; set; }
    }

    public class UserDto
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("organizationId")]
        public int OrganizationId { get; set; }

        [JsonPropertyName("organizationName")]
        public string OrganizationName { get; set; } = string.Empty;

        [JsonPropertyName("organizationType")]
        public string OrganizationType { get; set; } = string.Empty;

        [JsonPropertyName("roleName")]
        public string RoleName { get; set; } = string.Empty;
    }
}
