using AeroVeloz.Domain.Common.codeError.codeErrorSubscriptions;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Validators.interfaces.Subscriptions;

namespace AeroVeloz.Domain.Validators.Orquestador.Subscriptions
{
    public class SubscriptionValidatorImpl : ISubscriptionValidator
    {
        public ValidationResult ValidateCreate(short flightNumber, string codeAirlines, byte codeChannel, string contactValue)
        {
            var errors = new List<ErrosValidationResults>();

            if (flightNumber <= 0 || string.IsNullOrWhiteSpace(codeAirlines))
                errors.Add(ErrorSubscriptions.InvalidFlightReference);

            if (string.IsNullOrWhiteSpace(contactValue))
                errors.Add(ErrorSubscriptions.MissingContactValue);

            if (!Enum.IsDefined(typeof(SubscriptionChannel), codeChannel) || codeChannel == (byte)SubscriptionChannel.None)
                errors.Add(ErrorSubscriptions.InvalidSubscriptionChannel);

            if (errors.Count > 0)
                return new ValidationResult().Failur(errors);

            return new ValidationResult().Success();
        }

        public ValidationResult ValidateCancel(Guid subscriptionId)
        {
            if (subscriptionId == Guid.Empty)
                return new ValidationResult().Failur(ErrorSubscriptions.SubscriptionNotFound);

            return new ValidationResult().Success();
        }
    }
}
