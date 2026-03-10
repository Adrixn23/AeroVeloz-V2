using AeroVeloz.Domain.Entities.BaseEntity;
namespace AeroVeloz.Domain.Entities.Notification;

/// <summary>
/// Entidad de dominio que representa una notificación enviada a un suscriptor.
/// Cada notificación está vinculada a una suscripción, contiene el mensaje enviado,
/// el proveedor utilizado para el envío y el estado actual de la notificación.
/// </summary>
public partial class Notification : BEntity<Guid>
{
    public Guid subscriptionId { get; init; }

    public byte codeProvider { get; init; }

    public string? message { get; init; }

    public DateTime createAt { get; init; }

    public string? statusNotification {  get; init; }
}
