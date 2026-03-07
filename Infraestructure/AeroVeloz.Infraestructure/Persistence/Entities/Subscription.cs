using System;
using System.Collections.Generic;

namespace AeroVeloz.Infraestructure.Persistence.Entities;

public partial class Subscription
{
    public Guid SubscripcionId { get; set; }

    public short FlightNumber { get; set; }

    public string CodeAirlines { get; set; } = null!;

    public byte CodeChannel { get; set; }

    public int NumberInterested { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime EndingDate { get; set; }

    public bool ActiveSubscription { get; set; }

    public string ContactValue { get; set; } = null!;

    public virtual Airline CodeAirlinesNavigation { get; set; } = null!;

    public virtual ChannelSubscriptionNotification CodeChannelNavigation { get; set; } = null!;

    public virtual Flight FlightNumberNavigation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
