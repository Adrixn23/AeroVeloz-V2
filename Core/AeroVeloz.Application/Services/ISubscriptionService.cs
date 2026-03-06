using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Domain.ValidationBase;


namespace AeroVeloz.Application.Services
{
     public interface ISubscriptionService
    {



        ValidationResult ValidateSubscriptionCreation(
        Flights flight,
        Subscription newSubscription,
        IReadOnlyCollection<Subscription> existingSubscriptions,
        DateTime serverTime);

        ValidationResult ValidateSubscriptionCancellation(
            Subscription subscription,
            DateTime serverTime);

        ValidationResult ValidateNotificationEligibility(
            Flights flight,
            Subscription subscription,
            DateTime serverTime);
    }
}
