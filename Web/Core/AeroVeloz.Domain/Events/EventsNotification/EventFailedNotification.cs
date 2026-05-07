using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.Events.EventsNotification
{// Cuando el proveedor da error y se programa el Retry
    public record EventFailedNotification
    
        (
        Guid NotificationId, // id unico de la noti que fallo
        Guid SubscriptionId, // a qn iba dirigida
         short FlightNumber, // el vuelo q era 
        string TransportChannel, // el transporte de mensajeria
        string Message, // el mensaje
        string ErrorReason, // la razon del error
        int RetryCount, // INTENTOS fallidos: 
        DateTimeOffset FailedAt // cuando fallo
        
    ) : INotification;


    
}
