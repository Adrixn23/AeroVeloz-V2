using AeroVeloz.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroVeloz.Domain.Entities.Notification
{
    /// <summary>
    /// Entidad de dominio que representa un proveedor de envío de notificaciones.
    /// Define los canales disponibles para el despacho de notificaciones
    /// (ej: Email, SMS, Push Notification).
    /// </summary>
    /// 

    [Table("ProviderResponse", Schema = "Notifications")]

    public partial class ProviderResponse: BEntity<short>
    {
        public string? name { get; init; }
    }
}
