using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities.Subscriptions;

public partial class ViewActiveSubscription
{
    public string FlightNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsActive { get; set; }
}
