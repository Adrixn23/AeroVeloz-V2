using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Cuando el proveedor externo, ej. Email, confirma el envio
namespace AeroVeloz.Domain.Events.EventsNotification
{
   public record EventSendNotification(
            
        Guid NotificationId,
        Guid SubscriptionId, 
        Guid FlightId,              // Agregue el ID del vuelo para contexto
        string FlightStatus,        // El nuevo estado del vuelo
        string TransportChannel,    // esteemail "SignalR", "SMS", etc.
        string Message,             // elMensaje a enviar
        DateTimeOffset SentAt // registrar cuando se hizo la notificacion
    ) : INotification;

}
