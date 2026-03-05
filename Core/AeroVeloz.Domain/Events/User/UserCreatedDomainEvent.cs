using AeroVeloz.Domain.Entities.Users.Roles;
//using MediatR;

namespace AeroVeloz.Domain.Events.User
{
    public record UserCreatedDomainEvent(
        Guid idUser,
        string? codeAirport,
        Roles Role,
        DateTime createAt
        );
        //) : INotification;
    
}
