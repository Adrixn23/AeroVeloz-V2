using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.Validators.interfaces.Subscriptions
{
    public interface ISubscriptionValidator
    {
        ValidationResult ValidateCreate(short flightNumber, string codeAirlines, byte codeChannel, string contactValue);
        ValidationResult ValidateCancel(Guid subscriptionId);
    }
}
