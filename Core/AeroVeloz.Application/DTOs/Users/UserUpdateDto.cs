namespace AeroVeloz.Application.DTOs.Users
{
    public sealed record UserUpdateDto
    {
        public Guid idUser { get; set; }
        public string? nameUser { get; set; }
        public string? password {  get; set; }
        public bool isActive { get; set; }  
    }
}
