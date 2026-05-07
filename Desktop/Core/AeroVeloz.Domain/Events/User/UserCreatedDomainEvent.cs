using MediatR;

namespace AeroVeloz.Domain.Events.User
{
    public sealed record UserCreatedDomainEvent(
        Guid IdUser,
        string? NameUser,
        int IdOrganization,
        string? NameOrganization,
        string? TypeOrganization,
        short IdRol,
        string? NameRol,
        DateTime CreatedAt
    ) : INotification;
}
