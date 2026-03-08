using AeroVeloz.Domain.Entities.Organization.Base;
namespace AeroVeloz.Domain.Entities.Organization.Airports
{

    /// <summary>
    /// Entidad de dominio que representa un aeropuerto registrado en el sistema.
    /// los códigos ICAO/IATA, ubicación geográfica y la API key maestra para integraciones.
    /// </summary>
    public partial class Airport : Organizations
    {
        public string? codeAirportIcao { get; init; }

        public string? codeAirportIata { get; init; }

        public string? country { get; init; }

        public string? city { get; init; }

        public string? apiKeyMaster {  get; init; }

        public DateTimeOffset timeOffset { get; init; }

    }


}


