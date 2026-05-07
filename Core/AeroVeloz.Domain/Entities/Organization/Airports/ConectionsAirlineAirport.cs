using AeroVeloz.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Organization.Airport
{
    /// <summary>
    /// Entidad de dominio que representa la conexión (relación) entre una aerolínea y un aeropuerto.
    /// Permite gestionar qué aerolíneas operan en cada aeropuerto, incluyendo el token API
    /// de comunicación y el estado de la conexión.
    /// </summary>
    /// 


    [Table("ConectionsAirlineAirport", Schema = "Airport")]
    public class ConectionsAirlineAirport : BEntity<Guid>
    {
        public string? codeAirlinesIcao { get; init; }

        public string? codeAirportIcao { get; init;  }

        public string? tokenApi { get; init; }

        public bool isActive { get; init; }

        public DateTime createAt { get; init; }
    }
}
