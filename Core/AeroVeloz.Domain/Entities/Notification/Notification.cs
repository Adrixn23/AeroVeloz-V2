using AeroVeloz.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;
namespace AeroVeloz.Domain.Entities.Notification;

/// <summary>
/// Entidad de dominio que representa una notificación enviada a un suscriptor.
/// Cada notificación está vinculada a una suscripción, contiene el mensaje enviado,
/// el proveedor utilizado para el envío y el estado actual de la notificación.
/// </summary>
/// 


[Table("Notification", Schema = "Notifications")]

public partial class Notification : BEntity<Guid>
{
    public Guid subscripcionId { get; init; }

    public short codeProvider { get; init; }

    public string? message { get; init; }

    public DateTime createAt { get; init; }

    public string? statusNotification {  get; init; }
}
