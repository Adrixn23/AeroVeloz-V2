using MediatR;

namespace AeroVeloz.Domain.Events.User
{
   
    public sealed record UserDeactivatedDomainEvent(
        Guid IdUser,
        string? NameUser,
        int IdOrganization,
        string? NameOrganization,
        Guid IdUserResponsible,
        DateTime DeactivatedAt
    ) : INotification;
}
