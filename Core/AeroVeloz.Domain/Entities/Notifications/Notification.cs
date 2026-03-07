
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Subscriptions;
using System;
using System.Collections.Generic;




namespace AeroVeloz.Domain.Entities.Notifications;

public class Notification
{
   // Representa el intento fsico o digital de enviar una alerta a un usuario suscrito
      // Esta entidad actúa como un simple contenedor de datos Toda la lógica de negocio,
       // transiciones de estado y validaciones deben realizarse en los Servicios de Dominio o la aplicacion
    public Guid NotificationsId  { get; init; }
    public Guid SubscriptionId { get; init; }
    
          // Canal seleccionado para la entrega (Sms, email, Push/oneSignal 
           // Determina que adaptador de infraestructura se utilizará para el envío final
          
    public CodeProvidesNotifications CodeProvides { get; init; }  

   
          
    public string? Message { get; init; }  // Contenido final a ser leido por el pasajero o visitante. La palabra required elimina la advertencia de nulabilidad exigiendo
                                           // su inicialización al crear el objeto, pero manteniendo la entidad anémica.


    

    public DateTime CreateAt { get; init; } // fecha en la que el sistema orquesto la creación de la alerta
    /// Utilizado para medir la latencia de entrega y cumplir con los requisitos de rendimiento del sistema

    public NotificationDeliveryStatus StatusNotification { get; init; } //Controla el ciclo de vida del envío,. Pendiente, Enviado, Fallido
                                                                        //Esta propiedad debe ser actualizada manualmente por un servicio orquestador

}
