using AeroVeloz.Domain.Common.Enums.Organization;

namespace AeroVeloz.Domain.TransitionPolices
{
    public interface IChangeTypePolicy
    {
        public bool IsAllowed(OperationalChangeType type);
    }
}
