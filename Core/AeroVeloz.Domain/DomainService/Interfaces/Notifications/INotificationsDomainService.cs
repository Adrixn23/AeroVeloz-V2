using AeroVeloz.Domain.Common.Validation;

namespace AeroVeloz.Domain.DomainService.Interfaces.Notifications
{
    public interface INotificationsDomainService
    {
        Task<ValidationResult> IsValidChannelAsync(byte codeProvider);
        Task<ValidationResult> HasActiveSubscribersAsync(short flightNumber, string codeAirlines);
    }
}
