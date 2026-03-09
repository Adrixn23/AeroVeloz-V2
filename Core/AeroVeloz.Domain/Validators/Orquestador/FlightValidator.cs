using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainService.Interfaces.Flight;
using AeroVeloz.Domain.Validators.interfaces.Flight;
using AeroVeloz.Domain.Common.codeError.codeErrorFlights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DomainFlight = AeroVeloz.Domain.Entities.Flights.Flight;

namespace AeroVeloz.Domain.Validators.Orquestador.Flight
{
    public class FlightValidator : IFlightValidator
    {
        private readonly IFlightDomainService _flightDomainService;

        public FlightValidator(IFlightDomainService flightDomainService)
        {
            _flightDomainService = flightDomainService;
        }

        public async Task<ValidationResult> ValidateStateTransition(DomainFlight flight)
        {
            var errors = new List<ErrosValidationResults>();

            if (flight == null)
            {
                errors.Add(ErrorFlights.FlightNotFound);
                return new ValidationResult().Failur(errors);
            }

            // Validación de Puerta de Embarque (Salida)
            if (string.IsNullOrWhiteSpace(flight.BordingGate))
            {
                errors.Add(ErrorFlights.InvalidBoardingGate);
            }

            // Validación de Puerta de Llegada (Agregada!)
            if (string.IsNullOrWhiteSpace(flight.BoardingGateArrived))
            {
                errors.Add(ErrorFlights.InvalidArrivalGate);
            }

            // Validamos la transición lógica de estados (Ej: de Programado a En Vuelo)
            var transitionValid = await _flightDomainService.IsValidStatusTransitionAsync(flight, flight.flightStateId);
            if (!transitionValid.IsValid)
            {
                errors.Add(ErrorFlights.InvalidFlightState);
            }

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public async Task<ValidationResult> ValidateCreateAsync(DomainFlight flight)
        {
            var errors = new List<ErrosValidationResults>();

            //  Validaciones de formato y nulidad 
            if (flight == null)
            {
                errors.Add(ErrorFlights.FlightNotFound);
                return new ValidationResult().Failur(errors);
            }

            // Hereda el Id de BEntity<short>
            if (flight.Id <= 0)
                errors.Add(ErrorFlights.InvalidIdFlight);

            if (string.IsNullOrWhiteSpace(flight.codeAirlines))
                errors.Add(ErrorFlights.InvalidCodeAirlines);

            if (flight.OriginAirport == flight.DestinationAirport)
                errors.Add(ErrorFlights.SameOriginAndDestination);

            // Validaciones de fechas usando DateTimeOffset
            if (flight.ScheduledDeparture == default)
            {
                errors.Add(ErrorFlights.DepartureRequired);
            }
            else if (flight.ScheduledDeparture < DateTimeOffset.UtcNow)
            {
                errors.Add(ErrorFlights.DepartureInPast);
            }

            // Si hay errores básicos, cortamos aquí
            if (errors.Any()) return new ValidationResult().Failur(errors);


            // Validaciones profundas de negocio o abse de datos

            var originValid = await _flightDomainService.IsValidOriginAirportAsync(flight.OriginAirport!);
            if (!originValid.IsValid)
            {
                errors.Add(ErrorFlights.InvalidOrigin);
            }

            // Validación del Destino 
            var destValid = await _flightDomainService.IsValidDestinationAirportAsync(flight.DestinationAirport!);
            if (!destValid.IsValid)
            {

                errors.Add(ErrorFlights.InvalidDestination);
            }

            var isOwner = await _flightDomainService.IsAirlineOwnerOfFlightAsync(flight.Id, flight.codeAirlines!);
            if (!isOwner)
            {
                errors.Add(ErrorFlights.InvalidOwner);
            }

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();
        }

        public async Task<ValidationResult> ValidateFlightRowAsync(DomainFlight flight)
        {
            return await ValidateCreateAsync(flight);
        }
    }
}