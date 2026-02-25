using AeroVeloz.Domain.Common.Enums;

using MediatR;
namespace AeroVeloz.Domain.Events.User
{
    public record UserCreatedDomainEvent(
        Guid idUser,
        string? codeAirport,
        Role Role,
        DateTime createAt
        ) : INotification;
    
}
