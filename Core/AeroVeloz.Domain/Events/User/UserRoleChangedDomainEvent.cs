using MediatR;

namespace AeroVeloz.Domain.Events.User
{
    public sealed record UserRoleChangedDomainEvent(
        Guid IdUser,
        string? NameUser,
        int IdOrganization,
        short PreviousRolId,
        string? PreviousRolName,
        short NewRolId,
        string? NewRolName,
        Guid IdUserResponsible,
        DateTime ChangedAt
    ) : INotification;
}
