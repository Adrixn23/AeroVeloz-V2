
using AeroVeloz.Domain.Common.Enums.Mensajeria;

namespace AeroVeloz.Domain.TransitionPolices
{
    public interface INotificationPolicy
    {
        public bool isAllowedProvider(ProviderResponde providerResponde);

        //agregar posibles logicas futuras de notification policy en caso de que los adaptadores lo requieran
    
    }
}
