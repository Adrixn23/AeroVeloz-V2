using AeroVeloz.Domain.Common.Enums.Organization;

namespace AeroVeloz.Domain.Models.Operational
{
    public sealed record OperationalModel(
        Guid userId,
        Guid operationalId,
        OperationalChangeType Operational,
        DateTime changeAt,
        string? cause
        );
    
}
