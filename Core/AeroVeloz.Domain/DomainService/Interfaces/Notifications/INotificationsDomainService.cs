using AeroVeloz.Domain.Common.Validation;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AeroVeloz.Domain.Entities.Notification;

namespace AeroVeloz.Domain.DomainService.Interfaces.Notifications
{
    public interface INotificationsDomainService
    {
        Task<ValidationResult> IsValidChannelAsync(byte codeProvider);
        Task<ValidationResult> HasActiveSubscribersAsync(short flightNumber, string codeAirlines);
    }
}
