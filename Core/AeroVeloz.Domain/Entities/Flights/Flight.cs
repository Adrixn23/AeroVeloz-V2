using AeroVeloz.Domain.Entities.BaseEntity;

namespace AeroVeloz.Domain.Entities.Flight;




public class Flight : BEntity<short>
{

    public string codeAirlines { get; private set; } = null!;
    public byte flightStatesId { get; private set; }
    public string OriginAirport { get; private set; } = null!;
    public string DestinationAirport { get; private set; } = null!;
    public DateTimeOffset ScheduledDeparture { get; private set; }

    public DateTimeOffset ScheduledArrival { get; private set; }

    public string BordingGate { get; private set; }
    public string BordingGateArrived { get; private set; }

    public Flight(short id, string codeAirlines, byte flightStatesId, string OriginAirport, string DestinationAirport, DateTimeOffset ScheduledDeparture, DateTimeOffset ScheduledArrival, string BordingGate, string BordingGateArrived )
    {

        this.Id = id;
        this.codeAirlines = codeAirlines;
        this.flightStatesId = flightStatesId;
        this.OriginAirport = OriginAirport;
        this.DestinationAirport = DestinationAirport;
        this.ScheduledDeparture = ScheduledDeparture;
        this.ScheduledArrival = ScheduledArrival;
        this.BordingGate = BordingGate;
        this.BordingGateArrived = BordingGateArrived;
       


    }

}
    
