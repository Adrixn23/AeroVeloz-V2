namespace AeroVeloz.Transversal.Monitoring
{

    public sealed record MonitoringLogEntry
    {
        public int? OrganizationId { get; init; }
        public Guid? UserId { get; init; }
        public string Source { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public string? Detail { get; init; }
        public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    }
}
