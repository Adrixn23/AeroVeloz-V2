using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Subscriptions;

public partial class Subscription : BEntity<Guid>
{
   public short flightNumber { get; init; }
   public string? codeAirlines { get; init; }
   public byte codeChannel { get; init; }
   public int numberInterested { get; init; }
   public DateTime createDate { get; init; }
   public DateTime endingDate { get; init; }
   public bool activeSubscription { get; init; }
   public string? contactValue { get; init; }
  
}
