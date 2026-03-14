using AeroVeloz.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Operations
{
    /// <summary>
    /// Entidad de dominio que representa un catálogo de tipos de cambio operacional.
    /// Define las categorías de operaciones que se pueden realizar sobre los vuelos
    /// (ej: cambio de puerta, retraso, cancelación, despegue, aterrizaje).
    /// </summary>
    /// 

    [Table("OperationalChangeType", Schema ="Operations")]

    public partial class OperationalChangeType:BEntity<short>
    {
        public string? name { get; init; }
    }
}
