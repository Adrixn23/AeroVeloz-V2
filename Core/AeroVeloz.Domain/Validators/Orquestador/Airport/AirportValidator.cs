using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.DomainServices.Interfaces.Airport;
using AeroVeloz.Domain.Services.Interfaces.Airport;
using AeroVeloz.Domain.Validators.interfaces.Airports;
using AeroVeloz.Domain.Common.CodeErrors.CodeErrors.Aiport;

namespace AeroVeloz.Domain.Validators.Orquestador.Airport
{
    public class AirportValidator : IAirportValidator
    {
        private readonly IDomainServiceAirport _domainServiceAirport;
        private readonly IAiportExternarDomainServiceValidator _aiportExternarDomainServiceValidator;
        public AirportValidator(IDomainServiceAirport domainServiceAirport, IAiportExternarDomainServiceValidator aiportExternarDomainServiceValidator)
        {

            _domainServiceAirport = domainServiceAirport;
            _aiportExternarDomainServiceValidator = aiportExternarDomainServiceValidator;
        }
        public async Task<ValidationResult> ValidateForCreateAirport(Entities.Organization.Airports.Airport airport)
        {
            var errors = new List<ErrosValidationResults>();

            if (airport == null)
            {
                errors.Add(AirportErrors.AirportInvalid);
                return new ValidationResult().Failur(errors);
            }

            // Validar campos basicos
            var hasIata = !string.IsNullOrWhiteSpace(airport.codeAirportIata);
            var hasIcao = !string.IsNullOrWhiteSpace(airport.codeAirportIcao);

            if (!hasIata && !hasIcao)
                errors.Add(AirportErrors.AirportCodeMissing);

            if (hasIata)
            {
                var iata = airport.codeAirportIata!.Trim();
                if (iata.Length != 3 || !iata.All(char.IsLetter))
                    errors.Add(AirportErrors.AirportIataInvalid);
            }

            if (hasIcao)
            {
                var icao = airport.codeAirportIcao!.Trim();
                if (icao.Length != 4 || !icao.All(char.IsLetter))
                    errors.Add(AirportErrors.AirportIcaoInvalid);
            }

            if (string.IsNullOrWhiteSpace(airport.country))
                errors.Add(AirportErrors.AirportCountryInvalid);

            if (string.IsNullOrWhiteSpace(airport.city))
                errors.Add(AirportErrors.AirportCityInvalid);

            // Si ya existen errores de formato o datos faltantes, retornar de inmediato
            if (errors.Any())
                return new ValidationResult().Failur(errors);

            // Validacion externa: verificar que el aeropuerto existe en la fuente externa
            var existsExternal = await _aiportExternarDomainServiceValidator.ValidateAirport(airport.codeAirportIata ?? string.Empty, airport.codeAirportIcao ?? string.Empty);
            if (!existsExternal)
                errors.Add(AirportErrors.AirportNotFoundExternal);

            // Validacion en base de datos: verificar si ya existe en la organización
            var existsInDb = await _domainServiceAirport.ExistAirportByOrganizations(airport.codeAirportIata, airport.codeAirportIcao);
            if (existsInDb)
                errors.Add(AirportErrors.AirportAlreadyExists);

            var result = new ValidationResult();
            return errors.Any() ? result.Failur(errors) : result.Success();

        }
    }
}