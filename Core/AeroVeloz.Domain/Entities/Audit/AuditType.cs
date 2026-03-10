using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Audit
{
    
    /// Entidad de dominio que representa un catálogo de tipos de auditoría.
    /// Define las categorías de eventos auditables dentro del sistema
    /// (ej: acceso de usuario, cambio operacional, cambio de estado de vuelo, evento del sistema).
    
    public partial class AuditType : BEntity<short>
    {
        public string? nameAudit { get; init; }
    }
}
