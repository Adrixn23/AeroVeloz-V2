using AeroVeloz.Domain.Common.Enums.Mensajeria;
using AeroVeloz.Domain.Entities.BaseEntity;
namespace AeroVeloz.Domain.Entities.Notification;

public partial class Notification : BEntity<Guid>
{
    public Guid SubscripcionId { get; private set; }
    public ProviderResponde provider { get; private set;}
    public string? message { get; private set; }
    public DateTime createAt { get; private set;  }
    public NotificacionDeliveryStatus notificacionDeliveryStatus { get; private set; }

    private Notification(Guid notification, Guid SubscripcionId, 
        ProviderResponde provider, string? message, DateTime createAt)
    {
        Id = notification;
        this.SubscripcionId = SubscripcionId;
        this.provider = provider;
        this.message = message;
        this.createAt = createAt;
    }

    public static Notification create(Guid notification, Guid SubscripcionId,
        ProviderResponde provider, string? message, DateTime createAt)
    {
        return new Notification(notification,  SubscripcionId,
         provider,  message,  createAt);
    }    
}
