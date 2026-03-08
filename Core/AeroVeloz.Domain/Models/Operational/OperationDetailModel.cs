
namespace AeroVeloz.Domain.Models.Operational
{
    public sealed record OperationalDetailModel(
        Guid userId,
        Guid operationalId,
        string? nameAirport,
        string? Operational,
        DateTime changeAt,
        string? cause

        );
}
