using AeroVeloz.Domain.Entities.Flight;

//using MediatR;

namespace AeroVeloz.Domain.Events.Operations.SecurityOperationEvents
{
    public record OperationalDelayDetectedEvent(

       int flightNumber,
       string? codeAirline,
       string? codeAirport,
       DateTimeOffset OriginalScheduledTime,
       DateTimeOffset NewScheduledTime,
       string? cause,
       //TypeDealy delay,
       int idUserOperational,
       List<Entities.Flight.Flights> AffectedFlightsConnection

        );
        //) : INotification;
    
}
