using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class Notification
{
    public Guid NotificationsId { get; set; }

    public Guid SubscripcionId { get; set; }

    public short CodeProvider { get; set; }

    public string Message { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public int StatusNotification { get; set; }

    public virtual ProviderResponse CodeProviderNavigation { get; set; } = null!;

    public virtual Subscription Subscripcion { get; set; } = null!;
}
