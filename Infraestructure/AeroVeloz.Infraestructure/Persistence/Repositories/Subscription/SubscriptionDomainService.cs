using AeroVeloz.Domain.Common.codeError.codeErrorSubscriptions;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainService.Interfaces.Subscriptions;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Subscription
{
    public class SubscriptionDomainServiceImpl : ISubscriptionsDomainService
    {
        private readonly AeroVelozContext _context;

        public SubscriptionDomainServiceImpl(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> ValidateSubscriptionAsync(
            short flightNumber, string codeAirlines, SubscriptionChannel channel, string contactValue)
        {
            if (string.IsNullOrWhiteSpace(contactValue))
                return new ValidationResult().Failur(
                    ErrosValidationResults.Create("SUB_CONTACT", "El valor de contacto es requerido"));
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> ValidateCancellationAsync(Guid subscriptionId)
        {
            var exists = await _context.Subscriptions.AnyAsync(s => s.Id == subscriptionId && s.activeSubscription);
            if (!exists)
                return new ValidationResult().Failur(
                    ErrosValidationResults.Create("SUB_NOT_FOUND", "Suscripción no encontrada o ya cancelada"));
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> ValidateFlightAcceptsSubscriptionsAsync(short flightNumber, string codeAirlines)
        {
            var flight = await _context.Flights.AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == flightNumber && f.codeAirlinesIcao == codeAirlines);

            if (flight == null)
                return new ValidationResult().Failur(
                    ErrosValidationResults.Create("SUB_FLIGHT_MISSING", "Vuelo no encontrado"));

            byte[] closedStates = [6, 7]; // Completed, Cancelled
            if (closedStates.Contains(flight.flightStatesId))
                return new ValidationResult().Failur(
                    ErrosValidationResults.Create("SUB_FLIGHT_CLOSED", "El vuelo ya finalizó o fue cancelado"));

            if (flight.ScheduledDeparture < DateTimeOffset.UtcNow.AddHours(-2))
                return new ValidationResult().Failur(
                    ErrosValidationResults.Create("SUB_FLIGHT_PAST", "El vuelo ya salió y no acepta nuevas suscripciones"));

            return new ValidationResult().Success();
        }
    }
}
