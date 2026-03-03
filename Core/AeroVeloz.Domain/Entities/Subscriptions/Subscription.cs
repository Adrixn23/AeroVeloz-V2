using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Notifications;
using System;
using System.Collections.Generic;

namespace AeroVeloz.Domain.Entities.Subscriptions;

public class Subscription
{
   public Guid SubscriptionId { get; init; }

    public short FlightNumber { get; init; }

    public string? codeAirlines { get; init; }

    public short CodeChannel { get; init; }

    public int NumberInsterested { get; init; }

    public DateTime CreateDate { get; init; }

    public DateTime endingDate { get; init; }

   public short activeSubscription { get; init; }

   public string? contactValue { get; init; }
} 
