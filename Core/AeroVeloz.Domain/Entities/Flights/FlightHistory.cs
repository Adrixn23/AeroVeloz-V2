using AeroVeloz.Domain.Common.Enums;
using System;

namespace AeroVeloz.Domain.Entities.Flight;

public partial class FlightHistory
{
    // Llaves primarias compuestas
    public short FlightNumber { get; init; }
    public string? CodeAirlines { get; init; } 
    
    public DateTime ChangeAt { get;init; }
    public string? Reason { get; init; } 
    
    public FlightStateEnum FlightStatesIdAfter { get; init; }
    public FlightStateEnum FlightStatedsIdBefore { get; init; }


}
