namespace AeroVeloz.Desktop.Models.DTOs.Audit;

/// <summary>
/// DTO que mapea los datos de auditoría desde el API para la capa de presentación.
/// Contiene información sobre las acciones realizadas sobre usuarios.
/// </summary>
public class AuditDto
{
    public Guid AuditId { get; set; }
    public string? AuditTypeName { get; set; }
    public Guid IdUser { get; set; }
    public string? NameUser { get; set; }
    public int IdOrganization { get; set; }
    public string? NameOrganization { get; set; }
    public string? NameEntity { get; set; }
    public DateTime OccurredAt { get; set; }
    public string? DataNew { get; set; }
}
