<<<<<<< HEAD
﻿using AeroVeloz.Domain.Common.Enums.Organization;
using MediatR;
=======
﻿using AeroVeloz.Domain.Entities.Users.Roles;
//using MediatR;

>>>>>>> modulo-aeropuertuario
namespace AeroVeloz.Domain.Events.User
{
    public record UserCreatedDomainEvent(
        Guid idUser,
        string? codeAirport,
        Roles Role,
        DateTime createAt
<<<<<<< HEAD
        ) : INotification;
=======
        );
        //) : INotification;
>>>>>>> modulo-aeropuertuario
    
}
