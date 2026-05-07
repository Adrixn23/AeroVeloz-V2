using MediatR;

namespace AeroVeloz.Domain.Events.Operations
{

    public sealed record OperationalChangeRegisteredDomainEvent(
        Guid OperationId,
        Guid IdUser,
        short FlightNumber,
        string? CodeAirline,
        string? CodeAirport,
        string? OperationalTypeName,
        string? PreviousValue,
        string? NewValue,
        string? Cause,
        DateTime RegisteredAt
    ) : INotification;
}
