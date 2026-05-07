using AeroVeloz.Application.DTOs.Subscriptions;
using AeroVeloz.Application.Services.Result;

namespace AeroVeloz.Application.Contracts.Subscriptions
{
    public interface ISubscriptionService
    {
        Task<OperationResult<bool>> SubscribeExternalAsync(SubscriptionSaveDto dto);
        Task<OperationResult<bool>> CancelSubscriptionAsync(Guid subscriptionId);
        Task<OperationResult<IReadOnlyCollection<SubscriptionReadDto>>> GetByFlightAsync(short flightNumber, string codeAirlines);
        Task<OperationResult<int>> GetInterestedCountAsync(short flightNumber, string codeAirlines);
    }
}
