using MediatR;

namespace AeroVeloz.Domain.Events.User
{
    public sealed record UserAccountLockedDomainEvent(
        Guid IdUser,
        string? NameUser,
        int IdOrganization,
        string? NameOrganization,
        DateTime LockedUntil,
        int FailedAttempts,
        DateTime OccurredAt
    ) : INotification;
}
