using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Notification
{
    /// <summary>
    /// Entidad de dominio que representa un proveedor de envío de notificaciones.
    /// Define los canales disponibles para el despacho de notificaciones
    /// (ej: Email, SMS, Push Notification).
    /// </summary>
    public partial class ProviderResponse: BEntity<byte>
    {
        public string? name { get; init; }
    }
}
