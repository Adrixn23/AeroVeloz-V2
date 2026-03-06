using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Domain.Common.Validation;


namespace AeroVeloz.Application.Services
{
     public interface ISubscriptionService
    {



        ValidationResult ValidateSubscriptionCreation(
        Flight flight,
        Subscription newSubscription,
        IReadOnlyCollection<Subscription> existingSubscriptions,
        DateTime serverTime);

        ValidationResult ValidateSubscriptionCancellation(
            Subscription subscription,
            DateTime serverTime);

        ValidationResult ValidateNotificationEligibility(
            Flight flight,
            Subscription subscription,
            DateTime serverTime);
    }
}
