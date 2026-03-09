namespace AeroVeloz.Application.DTOs.Subscriptions
{
    public sealed record SubscriptionReadDto(
        Guid SubscriptionId,
        short FlightNumber,
        string? CodeAirlines,
        string? ChannelName,
        string? ContactValue,
        bool IsActive,
        DateTime CreatedAt
    );
}
