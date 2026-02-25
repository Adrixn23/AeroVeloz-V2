using System;

namespace AeroVeloz.Domain.Entities.Flight;

public partial class FlightHistory
{
    // Llaves primarias compuestas
    public short FlightNumber { get; private set; }
    public string CodeAirlines { get; private set; } = null!;
    
    public DateTime ChangeAt { get; private set; }
    public string Reason { get; private set; } = null!;
    
    public byte FlightStatesIdAfter { get; private set; }
    public byte FlightStatedsIdBefore { get; private set; }

    protected FlightHistory() { }

    public FlightHistory(short flightNumber, string codeAirlines, DateTime changeAt, string reason, byte stateIdAfter, byte stateIdBefore)
    {
        FlightNumber = flightNumber;
        CodeAirlines = codeAirlines;
        ChangeAt = changeAt;
        Reason = reason;
        FlightStatesIdAfter = stateIdAfter;
        FlightStatedsIdBefore = stateIdBefore;
    }
}
