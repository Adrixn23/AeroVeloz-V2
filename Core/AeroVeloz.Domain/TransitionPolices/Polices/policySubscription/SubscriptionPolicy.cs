using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Subscriptions;
using System;
using AeroVeloz.Domain.ValidationBase;
using AeroVeloz.Domain.Validators.codeError.codeErrorSubscriptions;

namespace AeroVeloz.Domain.TransitionPolices.Polices.policySubscrption
{
    public class SubscriptionPolicy : ISubscriptionPolicy
    {

        // recibe los datos mínimos exigidos para visitantes.
        public ValidationResult CanSubscribe(Flight flight, SubscriptionChannel channel, string contactValue)
        {
            var result = new ValidationResult();

            if (flight == null) 
            {
                return result.Failur(ErrorSubscriptions.InvalidFlightReference);
            }

            if (string.IsNullOrWhiteSpace(flight.codeAirlines) || flight.Id <= 0)
                  {
                       return result.Failur(ErrorSubscriptions.InvalidFlightReference);
                    }

            if (string.IsNullOrWhiteSpace(contactValue)) // . Protege contra nulos, vacioss y espacios en blanco en el correo o teléfono
            {
                return result.Failur(ErrorSubscriptions.MissingContactValue);
            }
            // uso del Enum para evitar que el canal quede sin asignar
            if (channel == SubscriptionChannel.None)
            {
                return result.Failur(ErrorSubscriptions.InvalidSubscriptionChannel);
            }
            // Si pasa todos los filtros, que me de lu velde
            return result.Success();
        }

       
        public ValidationResult CanCancel(Subscription subscription)
        {
            var result = new ValidationResult();

           //  Protege contra una buqueda en la base de datos que no arrojo resultadO
            if (subscription == null)
            {
                return result.Failur(ErrorSubscriptions.SubscriptionNotFound);
            }
            //  si ya está cancelada, no volvemos a cancelarla denuevoos
            if (!subscription.ActiveSubscription)
            {
                return result.Failur(ErrorSubscriptions.SubscriptionAlreadyCanceled);
            }

            return result.Success();
        }
        // Evalua el ciclo de vida del vuelo
        public ValidationResult IsFlightEligibleForSubscription(Flight flight, DateTime serverTime)
        {
            var result = new ValidationResult();


            if (flight == null)
            {
                return result.Failur(ErrorSubscriptions.InvalidFlightReference);
            }

            // El estado del vuelo es lo que realmente importa para saber si está cerrao
            if (flight.FlightStated == FlightStateEnum.AterrizadoArribado || flight.FlightStated == FlightStateEnum.Cancelado || flight.FlightStated == FlightStateEnum.Finalizado)
            {
                return result.Failur(ErrorSubscriptions.FlightAlreadyClosed);
            }



            return result.Success();


        }

        public ValidationResult IsNotificationAllowed(Flight flight, Subscription subscription)
        {
            var result = new ValidationResult();

            // valide 2 objeto a la ve
            if (flight == null || subscription == null)
            {
                return result.Failur(ErrorSubscriptions.SubscriptionNotFound);
            }
            // Si el usuario ya no esta suscrito, cortamos el envio de inmediato
            if (!subscription.ActiveSubscription)
            {
                return result.Failur(ErrorSubscriptions.SubscriptionAlreadyCanceled);
            }

            

            return result.Success();
        }
    }
}
