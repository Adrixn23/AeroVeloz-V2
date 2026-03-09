using AeroVeloz.Domain.Common.codeError.codeErrorFlights;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Validators.interfaces.Flight;

namespace AeroVeloz.Domain.Validators.Orquestador.Flights
{
    public class FlightValidatorImpl : IFlightValidator
    {
        private static readonly HashSet<(byte from, byte to)> AllowedTransitions =
        [
            ((byte)FlightStateEnum.Scheduled, (byte)FlightStateEnum.Boarding),
            ((byte)FlightStateEnum.Scheduled, (byte)FlightStateEnum.Delayed),
            ((byte)FlightStateEnum.Scheduled, (byte)FlightStateEnum.Cancelled),
            ((byte)FlightStateEnum.Boarding,   (byte)FlightStateEnum.InFlight),
            ((byte)FlightStateEnum.Boarding,   (byte)FlightStateEnum.Delayed),
            ((byte)FlightStateEnum.Boarding,   (byte)FlightStateEnum.Cancelled),
            ((byte)FlightStateEnum.Delayed,    (byte)FlightStateEnum.Boarding),
            ((byte)FlightStateEnum.Delayed,    (byte)FlightStateEnum.Cancelled),
            ((byte)FlightStateEnum.InFlight,   (byte)FlightStateEnum.Landed),
            ((byte)FlightStateEnum.InFlight,   (byte)FlightStateEnum.Diverted),
            ((byte)FlightStateEnum.Landed,     (byte)FlightStateEnum.Completed),
            ((byte)FlightStateEnum.Diverted,   (byte)FlightStateEnum.Landed),
            ((byte)FlightStateEnum.Diverted,   (byte)FlightStateEnum.Cancelled),
        ];

        public ValidationResult ValidateFlightRow(Flight flight)
        {
            var errors = new List<ErrosValidationResults>();

            if (string.IsNullOrWhiteSpace(flight.codeAirlines))
                errors.Add(ErrorFlights.InvalidCodeAirlines);

            if (string.IsNullOrWhiteSpace(flight.OriginAirport))
                errors.Add(ErrorFlights.InvalidOrigin);

            if (string.IsNullOrWhiteSpace(flight.DestinationAirport))
                errors.Add(ErrorFlights.InvalidOrigin);

            if (flight.OriginAirport == flight.DestinationAirport
                && !string.IsNullOrWhiteSpace(flight.OriginAirport))
                errors.Add(ErrorFlights.SameOriginAndDestination);

            if (flight.ScheduledDeparture <= DateTimeOffset.UtcNow)
                errors.Add(ErrorFlights.DepartureInPast);

            if (errors.Count > 0)
                return new ValidationResult().Failur(errors);

            return new ValidationResult().Success();
        }

        public ValidationResult ValidateStateTransition(byte currentStateId, byte newStateId)
        {
            if (currentStateId == newStateId)
                return new ValidationResult().Failur(ErrorFlights.InvalidFlightState);

            if (currentStateId == (byte)FlightStateEnum.Completed ||
                currentStateId == (byte)FlightStateEnum.Cancelled)
                return new ValidationResult().Failur(ErrorFlights.InvalidFlightState);

            if (!AllowedTransitions.Contains((currentStateId, newStateId)))
                return new ValidationResult().Failur(ErrorFlights.InvalidFlightState);

            return new ValidationResult().Success();
        }
    }
}
