namespace AeroVeloz.Domain.Models.UserSystem
{
    public sealed record UserSystemModel(
        Guid userId,
        string? nameUser,
        bool isActiveUser
        );
 
}
