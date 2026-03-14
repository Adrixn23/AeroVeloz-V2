using AeroVeloz.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Audit
{
    /// <summary>
    /// Entidad de dominio que representa un catálogo de tipos de auditoría.
    /// Define las categorías de eventos auditables dentro del sistema
    /// (ej: acceso de usuario, cambio operacional, cambio de estado de vuelo, evento del sistema).
    /// </summary>
    /// 


    [Table("AuditType", Schema = "Audits")]

    public partial class AuditType 
    {

        [Key]
        public short idAuditType { get; init; }
        public string? nameAudit { get; init; }
    }
}
