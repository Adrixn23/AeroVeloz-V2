using MediatR;
using System;

namespace AeroVeloz.Domain.Events.EventsNotification
{

    /// Evento que se dispara cuando se crea una notificación para un vueloe
    // Se debera lanzar una ultima notificacion en la finalizacion del ciclo operario del vuelo notificando sobre la finalizacion de la subscripcion al vuelo en cuestion


    public record NotificationCreatedEvent(
        // ID unico de la notificación creada
        Guid NotificationId,

      ///ID del vuelo que generó la notificacion
      short FlightNumber,

        ///ID de la suscripción del usuario interesado
        Guid SubscriptionId,

        /// Nuevo estado del vuelo
        string FlightStatus,

        /// Descripción del cambio
        string Message,

        /// Fecha de creación de la notificación
        DateTimeOffset CreatedAt
    ) : INotification;
}