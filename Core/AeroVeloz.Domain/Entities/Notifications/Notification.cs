using AeroVeloz.Domain.Entities.Subscriptions;

namespace AeroVeloz.Domain.Entities.Notifications;

public partial class Notification
{
    public Guid NotificationId { get; set; }

    public string? TypeNotification { get; set; }

    public string? Message { get; set; }

    public string? Status { get; set; }

    public string? ProviderResponse { get; set; }

    public DateTime? CreateDate { get; set; }

    public Guid SubscriptionId { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;
}
