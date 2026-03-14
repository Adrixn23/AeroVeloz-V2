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

        public Task<ValidationResult> ValidateCreateAsync(Entities.Flights.Flight flight)
        {
            var errors = new List<ErrosValidationResults>();

            // Si mandan un vuelo vacieo, lo rebotamos
            if (flight == null)
            {
                errors.Add(ErrorFlights.FlightNotFound);
                return Task.FromResult(new ValidationResult().Failur(errors));
            }

            
           if (string.IsNullOrWhiteSpace(flight.codeAirlinesIcao))
                errors.Add(ErrorFlights.InvalidCodeAirlines);

        if (string.IsNullOrWhiteSpace(flight.OriginAirport))
                errors.Add(ErrorFlights.InvalidOrigin);

                    if (string.IsNullOrWhiteSpace(flight.DestinationAirport))
                errors.Add(ErrorFlights.InvalidDestination); 

                    if (flight.OriginAirport == flight.DestinationAirport
                && !string.IsNullOrWhiteSpace(flight.OriginAirport))
                errors.Add(ErrorFlights.SameOriginAndDestination);

         if (flight.ScheduledDeparture <= DateTimeOffset.UtcNow)
                errors.Add(ErrorFlights.DepartureInPast);

            // 3. Empacamo el resultado 
                  if (errors.Count > 0)
                return Task.FromResult(new ValidationResult().Failur(errors));

            return Task.FromResult(new ValidationResult().Success());
        }

        

        public  Task<ValidationResult> ValidateFlightRowAsync(Entities.Flights.Flight flight)
        {
            var errors = new List<ErrosValidationResults>();

            if (string.IsNullOrWhiteSpace(flight.codeAirlinesIcao))
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
                return Task.FromResult(new ValidationResult().Success());

            return Task.FromResult(new ValidationResult().Success());
        }



        public Task<ValidationResult> ValidateStateTransition(byte currentStateId, byte newStateId)
        {
            var result = new ValidationResult();

            // No se puede cambiar al mismo estado actual
            if (currentStateId == newStateId)
            {
                return Task.FromResult(result.Failur(ErrorFlights.InvalidFlightState));
            }

            // Un vuelo Cancelado o Finalizado no puede volver a cambiar de estado
            
            if (currentStateId == (byte)FlightStateEnum.Completed ||
                currentStateId == (byte)FlightStateEnum.Cancelled)
            {
                return Task.FromResult(result.Failur(ErrorFlights.InvalidFlightState));
            }

            
           
            if (!AllowedTransitions.Contains((currentStateId, newStateId)))
            {
                return Task.FromResult(result.Failur(ErrorFlights.InvalidFlightState));
            }

            // Si pasó todos los filtros, devolvemos eexito
            return Task.FromResult(result.Success());
        }
    }
}
