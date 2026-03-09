using MediatR;

namespace AeroVeloz.Domain.Events.Audit
{
    public sealed record AuditDeletionAttemptedDomainEvent(
        Guid AuditId,
        Guid IdUserAttempted,
        string? NameUser,
        int IdOrganization,
        string? NameOrganization,
        string? EntityName,
        DateTime AttemptedAt
    ) : INotification;
}
