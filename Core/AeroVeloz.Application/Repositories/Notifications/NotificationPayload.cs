namespace AeroVeloz.Transversal.Contracts.Notifications
{
    public sealed record NotificationPayload
    {
        public string Title { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public string? Detail { get; init; }
        public int? OrganizationId { get; init; }
        public IReadOnlyCollection<string> TargetExternalIds { get; init; } = [];
        public ChannelType Channel { get; init; }
    }
}
