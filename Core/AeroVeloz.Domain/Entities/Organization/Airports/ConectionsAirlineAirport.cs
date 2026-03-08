using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Organization.Airport
{
    /// <summary>
    /// Entidad de dominio que representa la conexión (relación) entre una aerolínea y un aeropuerto.
    /// Permite gestionar qué aerolíneas operan en cada aeropuerto, incluyendo el token API
    /// de comunicación y el estado de la conexión.
    /// </summary>
    public class ConectionsAirlineAirport : BEntity<Guid>
    {
        public string? codeAirlines { get; init; }

        public string? codeAirport { get; init;  }

        public string? tokenApi { get; init; }

        public bool isActive { get; init; }

        public DateTime createAt { get; init; }
    }
}
