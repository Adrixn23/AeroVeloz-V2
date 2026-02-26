using AeroVeloz.Domain.Common.Enums.Mensajeria;
using AeroVeloz.Domain.Common.ValidationBase;

using AeroVeloz.Domain.Entities.Notification;

namespace AeroVeloz.Domain.Validators.interfaces.Notification
{
    public interface INotificationValidator
    {
        ValidationResult ValidationBodyNotification(AeroVeloz.Domain.Entities.Notification.Notification notification);
    }
}
