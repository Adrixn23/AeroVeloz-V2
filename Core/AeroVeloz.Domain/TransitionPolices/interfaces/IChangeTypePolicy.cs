using AeroVeloz.Domain.Common.Enums;

namespace AeroVeloz.Domain.TransitionPolices
{
    public interface IChangeTypePolicy
    {
        public bool IsAllowed(OperationalChangeType type);
    }
}
