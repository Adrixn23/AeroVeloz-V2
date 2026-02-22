using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Entities.Notifications;
using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities.Subscriptions;

public partial class Subscription
{
    public Guid SubscriptionId { get; set; }

    public Guid FlightId { get; set; }

    public string Email { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual Flight Flight { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
