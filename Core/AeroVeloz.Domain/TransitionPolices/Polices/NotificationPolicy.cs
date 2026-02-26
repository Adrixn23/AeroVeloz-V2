using AeroVeloz.Domain.Common.Enums.Mensajeria;
using AeroVeloz.Domain.TransitionPolices;

namespace AeroVeloz.Domain.Notifications
{
    public class NotificationPolicy : INotificationPolicy
    {
        private HashSet<ProviderResponde> AllowedTypes  = new HashSet<ProviderResponde>();

       
        public bool isAllowedProvider(ProviderResponde providerResponde)
        {
            return AllowedTypes.Contains(providerResponde);
        }

       
    }
}
