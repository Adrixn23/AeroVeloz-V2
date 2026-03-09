using AeroVeloz.Application.DTOs.Subscriptions;
using AeroVeloz.Domain.Entities.Subscriptions;

namespace AeroVeloz.Application.Repositories.Subscriptions
{
    public interface ISubscriptionRepository
    {
        Task<bool> CreateAsync(Subscription subscription);
        Task<bool> CancelAsync(Guid subscriptionId);
        Task<bool> ExistsDuplicateAsync(short flightNumber, string codeAirlines, byte codeChannel, string contactValue);
        Task<IReadOnlyCollection<Subscription>> GetActiveByFlightAsync(short flightNumber, string codeAirlines);
        Task<IReadOnlyCollection<SubscriptionReadDto>> GetSubscriptionsByFlightAsync(short flightNumber, string codeAirlines);
        Task<int> GetInterestedCountAsync(short flightNumber, string codeAirlines);
        Task<bool> CloseAllForFlightAsync(short flightNumber, string codeAirlines);
        Task<bool> AutoSubscribeAirlineAsync(short flightNumber, string codeAirlines, int organizationId);
    }
}
