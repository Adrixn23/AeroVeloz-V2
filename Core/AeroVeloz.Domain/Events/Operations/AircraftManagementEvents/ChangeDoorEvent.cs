//using MediatR;

namespace AeroVeloz.Domain.Events.Operations.AircraftManagementEvents
{
    public record ChangeDoorEvent(
            int FlightNumber,
            string? codeAirline,
            string? BordingGateDeparture,
            string? BordingGateArrived,
            DateTime? DateTime,
            string? cause,
            int idUser);
        //) : INotification;
    
}
