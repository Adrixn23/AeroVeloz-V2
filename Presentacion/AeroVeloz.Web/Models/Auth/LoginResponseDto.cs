namespace AeroVeloz.Web.Models.Auth
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = string.Empty;
        public DateTime ExpiresAtUtc { get; set; }
    }

    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string OrganizationType { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
