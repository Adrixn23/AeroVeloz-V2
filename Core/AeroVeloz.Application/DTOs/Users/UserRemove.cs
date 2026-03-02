namespace AeroVeloz.Application.DTOs.Users
{
    public record  UserRemove
    {
        public Guid idUser { get; set; }
        public bool isActive { get; set; } 
    }
}
