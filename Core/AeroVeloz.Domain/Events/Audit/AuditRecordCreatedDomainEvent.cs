using MediatR;

namespace AeroVeloz.Domain.Events.Audit
{
  
    public sealed record AuditRecordCreatedDomainEvent(
        Guid AuditId,
        short IdAuditType,
        string? AuditTypeName,
        Guid IdUser,
        int IdOrganization,
        string? NameEntity,
        DateTime OccurredAt
    ) : INotification;
}
