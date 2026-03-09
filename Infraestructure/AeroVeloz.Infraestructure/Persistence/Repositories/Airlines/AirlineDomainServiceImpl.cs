using AeroVeloz.Domain.Common.codeError.codeErrorAirlines;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainService.Interfaces.Airlines;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Airlines
{
    public class AirlineDomainServiceImpl : IAirlineDomainService
    {
        private readonly AeroVelozContext _context;

        public AirlineDomainServiceImpl(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> IsValidAirlineCodeAsync(string airlineCode)
        {
            var exists = await _context.Airlines.AsNoTracking()
                .AnyAsync(a => a.codeAirlines == airlineCode);
            if (!exists)
                return new ValidationResult().Failur(
                    ErrosValidationResults.Create("AIRLINE_CODE", $"Código de aerolínea no válido: {airlineCode}"));
            return new ValidationResult().Success();
        }

        public async Task<ValidationResult> HasConnectionWithAirportAsync(string codeAirlines, string airportCode)
        {
            var has = await _context.ConectionsAirlineAirports.AsNoTracking()
                .AnyAsync(c => c.codeAirlines == codeAirlines && c.codeAirport == airportCode && c.isActive);
            if (!has)
                return new ValidationResult().Failur(
                    ErrosValidationResults.Create("AIRLINE_CONNECTION", $"No existe conexión activa con el aeropuerto: {airportCode}"));
            return new ValidationResult().Success();
        }
    }
}
