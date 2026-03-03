using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Domain.TransitionPolices;
using AeroVeloz.Domain.ValidationBase;
using AeroVeloz.Domain.Validators.codeError.codeErrorNotifications;

namespace AeroVeloz.Domain.Notifications
{
    public class NotificationPolicy : INotificationPolicy
    {
     
        //agregar campos/atributos cuando se modifique lo descripto en la interfaz
        public ValidationResult ShouldNotify(OperationChange change, Flight flight)
        {
            var result = new ValidationResult();


            if (change == null) // si el cambio operativo esta nulo, entonces tirara un error que creamos en el CodeError de notifications. 
            {
                return result.Failur(ErrorNotifications.InsignificantOperationalChange);
            } 


            if (flight.FlightStated == FlightStateEnum.AterrizadoArribado ||
                flight.FlightStated == FlightStateEnum.Cancelado || 
                flight.FlightStated == FlightStateEnum.Finalizado)

            {
                return result.Failur(ErrorNotifications.FlightCycleClosed);
            }

            //  El cambio debe pertenecer a este vuelo específico
            if (change.flightNumber != flight.Id)
            {
                return result.Failur(ErrorNotifications.InvalidNotificationState);
            }
            // Validar que el cambio de puerta sea real y no redundante
            if (change.operationalChangeType == OperationalChangeType.GateChanged)
            {
                if (flight.BoardingGate == change.cause)
                {
                    return result.Failur(ErrorNotifications.InsignificantOperationalChange);
                }

            }

            return result.Success();
        }
        //agregar campos/atributos cuando se modifique lo descripto en la interfaz
        ValidationResult INotificationPolicy.IsRecipientAllowed(Guid flightId, Subscription subscription)
        {
            var result = new ValidationResult();

            if (subscription == null) // si la subscripcion esta vacia, entonces que envie este cod de error. 
                    {
                    return result.Failur(ErrorNotifications.InvalidSubscription);
                     }


            // verificamos y Usamos el error SubscriptionNotActive
            if (!subscription.ActiveSubscription == false)
            {
                return result.Failur(ErrorNotifications.MissingContactDestination);
            }
            // Validar que exista un medio de contacto(ej.Email)
            if (string.IsNullOrWhiteSpace(SubscriptionChannel.Email));
            {
                return result.Failur(ErrorNotifications.MissingContactDestination);
            }
            return result.Success();
        }
    }
}
