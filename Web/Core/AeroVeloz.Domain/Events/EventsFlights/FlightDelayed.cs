using AeroVeloz.Domain.Common.Enums;
using MediatR;

namespace AeroVeloz.Domain.Events.EventsFlights
{
    public record FlightDelayed(
        short FlightNumber,
        string AirlineCode,
        FlightStateEnum PreviousState,
        DateTimeOffset OriginalDeparture,
        DateTime DelayedAt
    ) : INotification;
}
