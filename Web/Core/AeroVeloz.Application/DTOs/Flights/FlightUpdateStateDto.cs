namespace AeroVeloz.Application.DTOs.Flights
{
    public sealed record FlightUpdateStateDto
    {
        public short FlightNumber { get; init; }
        public string? codeAirlinesIcao { get; init; }
        public byte FlightStateId { get; init; }
        public string? Reason { get; init; }
    }
}
