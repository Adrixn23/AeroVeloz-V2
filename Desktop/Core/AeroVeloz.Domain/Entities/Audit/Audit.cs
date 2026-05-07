using AeroVeloz.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Audit;
/// Entidad de dominio que representa un registro de auditoría del sistema.
/// Almacena información inmutable sobre las acciones realizadas por los usuarios,
/// incluyendo los valores anteriores y nuevos de las entidades modificadas.
/// Los registros de auditoría no pueden ser alterados una vez creados.
/// 


[Table("Audit", Schema = "Audits")]

public partial class Audit : BEntity<Guid>
{
    public short IdAuditType { get; init; }

    public Guid idUser { get; init; }

    public string? nameEntity { get; init; }

    public DateTime ocurrentAt { get; init; }

    public string? newValuesData { get; init; }

}