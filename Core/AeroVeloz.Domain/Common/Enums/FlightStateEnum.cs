namespace AeroVeloz.Domain.Common.Enums;

public enum FlightStateEnum : byte
{
    Scheduled = 1,
    InProgress = 2,
    Delayed = 3,
    InFlight = 4,
    Landed = 5,
    Finished = 6,
    Cancelled = 7,
    Diverted = 8
}
