namespace AeroVeloz.Application.DTOs.Subscriptions
{
    public sealed record SubscriptionCreateDto(
        short FlightNumber,
        string CodeAirlinesIcao,
        byte CodeChannel,
        string ContactValue,
        int NumberInterested
    );
}