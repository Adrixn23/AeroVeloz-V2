using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Operations
{
    /// <summary>
    /// Entidad de dominio que representa un catálogo de tipos de cambio operacional.
    /// Define las categorías de operaciones que se pueden realizar sobre los vuelos
    /// (ej: cambio de puerta, retraso, cancelación, despegue, aterrizaje).
    /// </summary>
    public partial class OperationalChangeType:BEntity<short>
    {
        public string? name { get; init; }
    }
}
