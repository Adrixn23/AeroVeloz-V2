using System.Text.Json.Serialization;

namespace AeroVeloz.Web.Models.Users
{
    public class UserStaffDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nameUser")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("idRol")]
        public short RoleId { get; set; }
    }

    public class CreateStaffDto
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("organizationId")]
        public int OrganizationId { get; set; }

        [JsonPropertyName("roleId")]
        public short RoleId { get; set; }
    }
}
