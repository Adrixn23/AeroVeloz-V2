using MediatR;

namespace AeroVeloz.Domain.Events.User
{
    public sealed record UserLoginFailedDomainEvent(
        Guid IdUser,
        string? NameUser,
        int IdOrganization,
        string? NameOrganization,
        int FailedAttempts,
        DateTime OccurredAt
    ) : INotification;
}
