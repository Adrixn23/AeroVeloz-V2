using MediatR;

namespace AeroVeloz.Domain.Events.Organization
{
   
    public sealed record OrganizationUpdatedDomainEvent(
        int IdOrganization,
        string? NameOrganization,
        string? TypeOrganization,
        string? EmailOrganization,
        bool IsActive,
        Guid IdUserResponsible,
        DateTime UpdatedAt
    ) : INotification;
}
