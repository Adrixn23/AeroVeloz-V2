namespace AeroVeloz.Application.DTOs.Subscriptions
{
    public sealed record SubscriptionReadDto(
        Guid Id,
        short FlightNumber,
        string CodeAirlinesIcao,
        byte CodeChannel,
        string ContactValue,
        int NumberInterested,
        DateTime CreateDate,
        bool ActiveSubscription
    );
}