using System;
using System.Collections.Generic;
using AeroVeloz.Domain.Common.ValidationBase;
using AeroVeloz.Domain.TransitionPolices;
using AeroVeloz.Domain.Validators.CodeErrors.CodeErrors.Notification;
using AeroVeloz.Domain.Validators.interfaces.Notification;

namespace AeroVeloz.Domain.Validators.Orquestador.Notifications
{
    public class NotificationValidator : INotificationValidator
    {
        private readonly INotificationPolicy _notificationPolicy;

        public NotificationValidator(INotificationPolicy notificationPolicy) {

            _notificationPolicy = notificationPolicy;
        }

        public ValidationResult ValidationBodyNotification(AeroVeloz.Domain.Entities.Notification.Notification notification)
        {
            var errors = new List<DomainError>();

            if (notification == null)
            {
                errors.Add(NotificationError.InvalidNotification);
                return new ValidationResult().Failur(errors);
            }

            if (string.IsNullOrEmpty(notification.message))
                errors.Add(NotificationError.InvalidMessage);

             if (notification.SubscripcionId == Guid.Empty)
                errors.Add(NotificationError.InvalidSubscription);

            if (!_notificationPolicy.isAllowedProvider(notification.provider)) 
                errors.Add(NotificationError.InvalidProvider);


            return errors.Any() ? new ValidationResult().Success() :  new ValidationResult().Failur(errors);
        }
    }
}
