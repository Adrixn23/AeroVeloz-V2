using System;
using System.Collections.Generic;
using AeroVeloz.Domain.Common.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroVeloz.Domain.Entities.Notifications;
namespace AeroVeloz.Domain.DomainService.Interfaces.Notifications
{
    public interface INotificationsDomainService
    {
        Task<ValidationResult> IsValidNotification(Notification notification); // que la validacion cuente con lo necesario, Cuerpo, destinario y canal Valido.

        Task<ValidationResult> CheckValidSubscription(Guid SubscriptionId); // verificar que la subscripcion este activa o sea valida. 


        Task<ValidationResult> IsValidChannel(string CodeProvides); // que en el canal de envio de notificaciones, solo sean esos canales registrados, nada mas. 

        bool HasActiveSubscribers(Guid flightId, IEnumerable<Guid> activeSubscriptionId); // cancelacion de elemento que orquesta las notificaciones
                                                                                           // si la subscripcion del vuelo no cuenta con nigun interesado 






    }
}
