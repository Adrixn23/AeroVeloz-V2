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
        
        public ValidationResult IsRecipientAllowed(Guid flightId, Subscription subscription, DateTimeOffset flightStatusChangedAt)
        {
            var result = new ValidationResult();

            if (subscription == null)
            {
                return result.Failur(ErrorNotifications.InvalidSubscription);
            }

            // devuelve si esta activo o no
            if (!subscription.ActiveSubscription)
            {
                return result.Failur(ErrorNotifications.SubscriptionNotActive);
            }

            // Validar que exista un medio de contacto (ej.Email, etx)
            if (string.IsNullOrWhiteSpace(subscription.ContactValue))
            {
                return result.Failur(ErrorNotifications.MissingContactDestination);
            }

            //  La validación de los 15 minutos del sad.
            if (flightStatusChangedAt.AddMinutes(15) < DateTimeOffset.UtcNow)
            {
                return result.Failur(ErrorNotifications.SlaTimeLimitBreached);
            }

            return result.Success();
        }
    }
}
