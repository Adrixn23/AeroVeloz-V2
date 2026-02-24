using System;
using AeroVeloz.Domain.Entities.BaseEntity;
using AeroVeloz.Domain.Entities.Exceptions;

namespace AeroVeloz.Domain.Entities.Flight;




public class Flight : BEntity<short>
{

    public string codeAirlines { get; private set; } = null!;
    public byte flightStatesId { get; private set; }
    public string OriginAirport { get; private set; } = null!;
    public string DestinationAirport { get; private set; } = null!;
    public DateTimeOffset ScheduledDeparture { get; private set; }

    public DateTimeOffset ScheduledArrival { get; private set; }

    public Flight (short id, string codeAirlines, byte flightStatesId, string OriginAirport,string DestinationAirport, DateTimeOffset ScheduledDeparture, DateTimeOffset ScheduledArrival )
    {

        this.Id = id;
        this.codeAirlines = codeAirlines;
        this.flightStatesId = flightStatesId;
        this.OriginAirport = OriginAirport;
        this.DestinationAirport = DestinationAirport;
        this.ScheduledDeparture = ScheduledDeparture;
        this.ScheduledArrival = ScheduledArrival;
       


    }

}
    
