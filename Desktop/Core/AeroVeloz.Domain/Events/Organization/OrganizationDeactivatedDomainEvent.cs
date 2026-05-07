using MediatR;

namespace AeroVeloz.Domain.Events.Organization
{
 
    public sealed record OrganizationDeactivatedDomainEvent(
        int IdOrganization,
        string? NameOrganization,
        string? TypeOrganization,
        string? EmailOrganization,
        Guid IdUserResponsible,
        DateTime DeactivatedAt
    ) : INotification;
}
