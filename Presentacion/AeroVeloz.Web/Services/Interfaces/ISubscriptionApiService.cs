using System.Net.Http.Headers;
using AeroVeloz.Web.Models.Subscriptions;
using AeroVeloz.Web.Services.Implementations;
using AeroVeloz.Web.Services.Interfaces;

namespace AeroVeloz.Web.Services.Interfaces
{
    public interface ISubscriptionApiService
    {
        Task<int> GetSubscriptionCountAsync(short flightNumber, string airlineCode, string token);
        Task<List<SubscriptionReadDto>> GetSubscriptionsByFlightAsync(short flightNumber, string airlineCode, string token);
        Task<bool> SubscribeExternalAsync(string email, short flightNumber, string airlineCode);
    }
}
