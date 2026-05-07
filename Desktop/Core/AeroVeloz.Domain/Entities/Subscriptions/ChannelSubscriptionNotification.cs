using AeroVeloz.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Subscriptions
{
    /// <summary>
    /// Entidad de dominio que representa un canal de notificación disponible para las suscripciones.
    /// Define los medios por los cuales los suscriptores pueden recibir notificaciones
    /// (ej: "EMAIL", "SMS", "PUSH").
    /// </summary>
    /// 


    [Table("ChannelSubscriptionNotification", Schema = "Subscriptions")]
    public partial class ChannelSubscriptionNotification: BEntity<byte>
    {
        public string? name { get; init; }
    }
}
