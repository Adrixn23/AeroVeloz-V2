using MediatR;

namespace AeroVeloz.Domain.Events.Operations
{
    public record OperationalChangeDeactivatedDomainEvent(
        Guid OperationId,
        Guid UserId,
        short FlightNumber,
        DateTime DeactivatedAt,
        string? Reason
    ) : INotification;
}
