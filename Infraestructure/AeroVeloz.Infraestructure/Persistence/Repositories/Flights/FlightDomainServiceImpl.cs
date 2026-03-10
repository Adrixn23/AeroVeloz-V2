using AeroVeloz.Domain.Common.codeError.codeErrorFlights;
using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainService.Interfaces.Flights;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Flights
{
    public class FlightDomainServiceImpl : IFlightDomainService
    {
        private readonly AeroVelozContext _context;

        public FlightDomainServiceImpl(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> IsValidOriginAirportAsync(string codeAirlines, string airportCode)
        {
            var hasConnection = await _context.ConectionsAirlineAirports.AsNoTracking()
                .AnyAsync(c => c.codeAirlines == codeAirlines && c.codeAirport == airportCode && c.isActive);
            if (!hasConnection)
                return new ValidationResult().Failur(
                    ErrosValidationResults.Create("FLIGHT_ORIGIN", $"La aerolínea no tiene conexión activa con el aeropuerto de origen: {airportCode}"));
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> IsValidDestinationAirportAsync(string codeAirlines, string airportCode)
        {
            var hasConnection = await _context.ConectionsAirlineAirports.AsNoTracking()
                .AnyAsync(c => c.codeAirlines == codeAirlines && c.codeAirport == airportCode && c.isActive);
            if (!hasConnection)
                return new ValidationResult().Failur(
                    ErrosValidationResults.Create("FLIGHT_DEST", $"La aerolínea no tiene conexión activa con el aeropuerto de destino: {airportCode}"));
            return new ValidationResult().Success();
        }

        public Task<ValidationResult> IsValidStatusTransitionAsync(byte currentStateId, FlightStateEnum newState)
        {
            var newStateId = (byte)newState;

           
            if (currentStateId == (byte)FlightStateEnum.InFlight && newStateId == (byte)FlightStateEnum.Cancelled)
                return Task.FromResult(new ValidationResult().Failur(
                    ErrosValidationResults.Create("FLIGHT_TRANSITION", "No se puede cancelar un vuelo que está en el aire")));

            if (currentStateId == (byte)FlightStateEnum.Landed && newStateId == (byte)FlightStateEnum.Cancelled)
                return Task.FromResult(new ValidationResult().Failur(
                    ErrosValidationResults.Create("FLIGHT_TRANSITION", "No se puede cancelar un vuelo que ya aterrizó")));

           
            if (currentStateId == (byte)FlightStateEnum.Completed || currentStateId == (byte)FlightStateEnum.Cancelled)
                return Task.FromResult(new ValidationResult().Failur(
                    ErrosValidationResults.Create("FLIGHT_TERMINAL", "El vuelo ya está en un estado terminal y no acepta más cambios")));

          
            if (newStateId < currentStateId && newStateId != (byte)FlightStateEnum.Cancelled && newStateId != (byte)FlightStateEnum.Diverted)
                return Task.FromResult(new ValidationResult().Failur(
                    ErrosValidationResults.Create("FLIGHT_BACKWARD", "Transición de estado no permitida: no se puede retroceder en el flujo")));

            return Task.FromResult(new ValidationResult().Success());
        }

        
    }
}