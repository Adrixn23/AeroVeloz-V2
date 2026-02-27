using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Common.Enums;



public class Flight : BEntity<short>
{

    public string codeAirlines { get; private set; } = null!;
    public FlightStateEnum FlightStated { get;  set; }
    public string OriginAirport { get; private set; } = null!;
    public string DestinationAirport { get; private set; } = null!;
    public DateTimeOffset ScheduledDeparture { get; private set; }

    public DateTimeOffset ScheduledArrival { get; private set; }

    public string BoardingGate { get; private set; }
    public string BoardingGateArrived { get; private set; }

    public Flight(short id, string codeAirlines, FlightStateEnum FlightStated, string OriginAirport, string DestinationAirport, DateTimeOffset ScheduledDeparture, DateTimeOffset ScheduledArrival, string BordingGate, string BordingGateArrived )
    {

        this.Id = id;
        this.codeAirlines = codeAirlines;
        this.FlightStated = FlightStated;
        this.OriginAirport = OriginAirport;
        this.DestinationAirport = DestinationAirport;
        this.ScheduledDeparture = ScheduledDeparture;
        this.ScheduledArrival = ScheduledArrival;
        this.BoardingGate = BordingGate;
        this.BoardingGateArrived = BordingGateArrived;
       


    }

    
    public void ChangeState(FlightStateEnum newState)
    {
        this.FlightStated = newState;
    }
}
