using AeroVeloz.Domain.Common.Enums;

namespace AeroVeloz.Domain.Events.User
{
    public  record UserCreatedDomainEvent(
        Guid idUser,
        string? codeAirport,
        Role Role,
        DateTime createAt
        ) : INotification
    
}
