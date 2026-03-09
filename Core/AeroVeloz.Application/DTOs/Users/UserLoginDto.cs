namespace AeroVeloz.Application.DTOs.Users
{
    public sealed record  UserLoginDto
    {
        public string? emailOrganization {  get; set; }
        public string? password { get; set; }
        public string? nameUser { get; set; }
        public byte[]? ipAddress { get; set; }
    }
}
