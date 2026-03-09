using AeroVeloz.Domain.Entities.BaseEntity;
namespace AeroVeloz.Domain.Entities.Notification;

public partial class Notification : BEntity<Guid>
{
    public Guid subscriptionId { get; init; }
    public byte codeProvider { get; init; }
    public string? message { get; init; }
    public DateTime createAt { get; init; }

    public string? statusNotification {  get; init; }
}
