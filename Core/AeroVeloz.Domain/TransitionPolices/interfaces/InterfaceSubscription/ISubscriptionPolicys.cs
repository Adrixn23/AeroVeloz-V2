using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Domain.ValidationBase;


namespace AeroVeloz.Domain.TransitionPolices.interfaces.InterfaceSubscription
{
    public interface ISubscriptionPolicy
    {
        //Desconectar el canal de subscripcion cuando se cree el enum correspondiente

        //metodo para manejas las politicas de subscripcion
         ValidationResult CanSubscribe(Flight flight, SubscriptionChannel channel, string contactValue);



        ValidationResult CanCancel(Subscription subscription);

        ValidationResult IsFlightEligibleForSubscription(Flight flight,DateTime serverTime);

        ValidationResult IsNotificationAllowed(Flight flight,Subscription subscription);


    }
}
