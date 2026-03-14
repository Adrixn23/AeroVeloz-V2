using AeroVeloz.Application.DTOs.Subscriptions;
using AeroVeloz.Application.Repositories.Subscriptions;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Subscription
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AeroVelozContext _context;

        public SubscriptionRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Domain.Entities.Subscriptions.Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CancelAsync(Guid subscriptionId)
        {
            var rows = await _context.Subscriptions
                .Where(s => s.Id == subscriptionId && s.activeSubscription)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.activeSubscription, false));
            return rows > 0;
        }

        public async Task<bool> ExistsDuplicateAsync(short flightNumber, string codeAirlines, byte codeChannel, string contactValue)
        {
            return await _context.Subscriptions.AsNoTracking()
                .AnyAsync(s => s.flightNumber == flightNumber
                    && s.codeAirlinesIcao == codeAirlines
                    && s.codeChannel == codeChannel
                    && s.contactValue == contactValue
                    && s.activeSubscription);
        }

        public async Task<IReadOnlyCollection<Domain.Entities.Subscriptions.Subscription>> GetActiveByFlightAsync(short flightNumber, string codeAirlines)
        {
            return await _context.Subscriptions.AsNoTracking()
                .Where(s => s.flightNumber == flightNumber && s.codeAirlinesIcao == codeAirlines && s.activeSubscription)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<SubscriptionReadDto>> GetSubscriptionsByFlightAsync(short flightNumber, string codeAirlines)
        {
            return await (
                from s in _context.Subscriptions.AsNoTracking()
                join c in _context.ChannelSubscriptionNotifications.AsNoTracking() on s.codeChannel equals c.Id
                where s.flightNumber == flightNumber && s.codeAirlinesIcao == codeAirlines && s.activeSubscription
                select new SubscriptionReadDto(
                    s.Id, s.flightNumber, s.codeAirlinesIcao, c.name,
                    s.contactValue, s.activeSubscription, s.createDate)
            ).ToListAsync();
        }

        public async Task<int> GetInterestedCountAsync(short flightNumber, string codeAirlines)
        {
            return await _context.Subscriptions.AsNoTracking()
                .Where(s => s.flightNumber == flightNumber && s.codeAirlinesIcao == codeAirlines && s.activeSubscription)
                .CountAsync();
        }

        public async Task<bool> CloseAllForFlightAsync(short flightNumber, string codeAirlines)
        {
            var rows = await _context.Subscriptions
                .Where(s => s.flightNumber == flightNumber && s.codeAirlinesIcao == codeAirlines && s.activeSubscription)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.activeSubscription, false)
                    .SetProperty(x => x.endingDate, DateTime.UtcNow));
            return rows >= 0;
        }

        public async Task<bool> AutoSubscribeAirlineAsync(short flightNumber, string codeAirlines, int organizationId)
        {
            var airlineSubscription = new Domain.Entities.Subscriptions.Subscription
            {
                Id = Guid.NewGuid(),
                flightNumber = flightNumber,
                codeAirlinesIcao = codeAirlines,
                codeChannel = 3, // Push
                contactValue = $"airline:{organizationId}",
                numberInterested = 1,
                createDate = DateTime.UtcNow,
                endingDate = DateTime.UtcNow.AddDays(30),
                activeSubscription = true
            };
            _context.Subscriptions.Add(airlineSubscription);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
