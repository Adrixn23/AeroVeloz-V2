namespace AeroVeloz.Application.DTOs.Subscriptions
{
    public sealed record SubscriptionSaveDto
    {
        public short FlightNumber { get; init; }
        public string? CodeAirlines { get; init; }
        public byte CodeChannel { get; init; }
        public string? ContactValue { get; init; }
    }
}
