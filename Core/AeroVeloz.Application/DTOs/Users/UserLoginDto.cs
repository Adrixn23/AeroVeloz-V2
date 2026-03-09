namespace AeroVeloz.Application.DTOs.Users
{
    public sealed record  UserLoginDto // DTO para trasnportar la data del usuario gestionada para el logueo del mismo dentro
                                       // del sitema y gestionando por su use cases correspondiente.
    {
        public string? emailOrganization {  get; set; }
        public string? password { get; set; }
        public string? nameUser { get; set; }

    }
}
