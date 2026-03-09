namespace AeroVeloz.Application.DTOs.Users
{
    public sealed record UserUpdateDto
    {
        public Guid IdUser { get; init; }
        public string? NameUser { get; init; }
        public string? Password { get; init; }
        public bool IsActive { get; init; }
        public short IdRol { get; init; }
        public int IdOrganization { get; init; }
    }
}
