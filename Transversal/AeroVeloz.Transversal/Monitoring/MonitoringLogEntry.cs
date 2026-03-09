namespace AeroVeloz.Transversal.Monitoring
{

    public sealed record MonitoringLogEntry(
        int? OrganizationId,
        Guid? UserId,
        string Source,
        string Message,
        string? Detail = null
    )
    {
        public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    }
}
