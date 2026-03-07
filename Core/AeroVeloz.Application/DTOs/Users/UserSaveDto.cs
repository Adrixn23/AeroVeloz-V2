namespace AeroVeloz.Application.DTOs.Users
{
    public sealed record UserSaveDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }   
    }
}
