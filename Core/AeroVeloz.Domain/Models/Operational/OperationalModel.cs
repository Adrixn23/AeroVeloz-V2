
namespace AeroVeloz.Domain.Models.Operational
{
    public sealed record OperationalModel(
        Guid userId,
        Guid operationalId,
        string? Operational,
        DateTime changeAt,
        string? cause
        );
    
}
