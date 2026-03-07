<<<<<<< HEAD
﻿using AeroVeloz.Domain.TransitionPolices;
=======
﻿
>>>>>>> modulo-aeropuertuario
using MediatR;
namespace AeroVeloz.Domain.Events.Aiport
{
    public record AirportRegisteredDomainEvent(
        string? codeAirport,
        string? codeAirportIATA,
        string? nameAiport,
        string? apiKeyMaster,
        string? createAt,
        string? emailOrganization
        ) : INotification;
  
}
