using MediatR;

namespace AeroVeloz.Domain.Events.User
{
    public sealed record UserUpdatedDomainEvent(
        Guid IdUser,
        string? NameUser,
        int IdOrganization,
        bool IsActive,
        bool PasswordChanged,
        Guid IdUserResponsible,
        DateTime UpdatedAt
    ) : INotification;
}
