using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.DomainService.Interfaces.Subscriptions
{
    public interface ISubscriptionsDomainService
    {
        Task<ValidationResult> ValidateSubscriptionAsync(short flightNumber, string codeAirlines, SubscriptionChannel channel, string contactValue);
        Task<ValidationResult> ValidateCancellationAsync(Guid subscriptionId);
        Task<ValidationResult> ValidateFlightAcceptsSubscriptionsAsync(short flightNumber, string codeAirlines);
    }
}