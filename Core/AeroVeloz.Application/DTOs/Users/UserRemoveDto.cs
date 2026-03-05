namespace AeroVeloz.Application.DTOs.Users
{
    public record  UserRemoveDto
    {
        public Guid idUser { get; set; }
        public bool isActive { get; set; }
        
        //DTO que permite desactivar un usuario determinado dentro del sistema
    }
}
