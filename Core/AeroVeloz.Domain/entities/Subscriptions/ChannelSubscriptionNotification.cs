using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Subscriptions
{
    /// <summary>
    /// Entidad de dominio que representa un canal de notificación disponible para las suscripciones.
    /// Define los medios por los cuales los suscriptores pueden recibir notificaciones
    /// (ej: "EMAIL", "SMS", "PUSH").
    /// </summary>
    public partial class ChannelSubscriptionNotification: BEntity<byte>
    {
        public string? name { get; init; }
    }
}
