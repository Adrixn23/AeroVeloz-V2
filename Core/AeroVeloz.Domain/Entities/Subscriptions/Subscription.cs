

namespace AeroVeloz.Domain.Entities.Subscriptions;

public class Subscription
{
   public Guid SubscriptionId { get; init; }

    public short FlightNumber { get; init; }

    public string? CodeAirlines { get; init; }

    public short CodeChannel { get; init; }

    public int NumberInsterested { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? CanceledAt { get; init; }

   public bool ActiveSubscription { get; init; }

   public string? ContactValue { get; init; }
} 
