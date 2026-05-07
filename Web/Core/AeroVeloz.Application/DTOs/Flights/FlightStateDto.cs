namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightStateDto(
        byte Id,
        string? Name,
        string? Description
    );
}

