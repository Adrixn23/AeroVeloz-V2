using AeroVeloz.Domain.Common.Enums.Organization;
using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Operations;

public partial class OperationChange :  BEntity<Guid>
{
      public Guid idUser { get; init; }
      public OperationalChangeType operationType { get; init; }   
      public short? flightNumber { get; init; }
      public string? codeAirline { get; init;  }
      public string? codeAirport { get; init; }
      public string? previosValue { get; init; }
      public string? newValue {  get; init; }
      public DateTime changeAt { get; init; }
      public string? cause { get; init; }
            
}
