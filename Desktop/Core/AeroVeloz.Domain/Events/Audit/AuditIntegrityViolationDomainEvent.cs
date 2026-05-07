using MediatR;

namespace AeroVeloz.Domain.Events.Audit
{
   
    public sealed record AuditIntegrityViolationDomainEvent(
        Guid AuditId,
        int IdOrganization,
        string? NameOrganization,
        string? EntityName,
        string? ViolationDetail,
        DateTime DetectedAt
    ) : INotification;
}
