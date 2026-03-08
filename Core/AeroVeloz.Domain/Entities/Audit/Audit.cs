using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Audits;

/// <summary>
/// Entidad de dominio que representa un registro de auditoría del sistema.
/// Almacena información inmutable sobre las acciones realizadas por los usuarios,
/// incluyendo los valores anteriores y nuevos de las entidades modificadas.
/// Los registros de auditoría no pueden ser alterados una vez creados.
/// </summary>
public partial class Audit : BEntity<Guid>
{
    public short IdAuditType { get;  init; }

    public Guid idUser { get;  init; }

    public string? nameEntity { get;  init; }

    public DateTime occurentAt { get;  init; }

    public string? oldValues { get;  init; }

    public string? DataOld { get; init; }

    public string? DataNew { get; init; }

}
