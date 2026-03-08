using MediatR;

﻿using AeroVeloz.Domain.Entities.Users.Roles;

namespace AeroVeloz.Domain.Events.User
{
    public record UserCreatedDomainEvent(
        Guid idUser,
        string? codeAirport,
        Roles Role,
        DateTime createAt
        ) : INotification;

       
      
    
}
