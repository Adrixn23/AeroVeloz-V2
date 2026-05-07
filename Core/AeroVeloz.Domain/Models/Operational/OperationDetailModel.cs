
namespace AeroVeloz.Domain.Models.Operational
{
    public sealed record OperationalDetailModel(
        Guid id,
        short idOperationalType,
        short flightNumber,
        string? codeAirlinesIcao,
        string? codeAirportIcao,
        string? previosValue,
        string? newValue,
        DateTime changeAt,
        string? cause,
        bool isActive
    );
}
