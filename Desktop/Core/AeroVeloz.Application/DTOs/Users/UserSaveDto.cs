namespace AeroVeloz.Application.DTOs.Users
{
    public sealed record UserSaveDto
    {
        public string? UserName { get; init; }
        public string? Password { get; init; }
        public int IdOrganization { get; init; }
        public short IdRol { get; init; }
    }
}
