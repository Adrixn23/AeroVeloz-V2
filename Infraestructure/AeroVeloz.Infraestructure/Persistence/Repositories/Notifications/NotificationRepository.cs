using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Domain.Entities.Notification;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AeroVelozContext _context;

        public NotificationRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStatusAsync(Guid notificationId, string newStatus)
        {
            var rows = await _context.Notifications
                .Where(n => n.Id == notificationId)
                .ExecuteUpdateAsync(s => s.SetProperty(n => n.statusNotification, newStatus));
            return rows > 0;
        }

        public async Task<IReadOnlyCollection<Notification>> GetBySubscriptionAsync(Guid subscriptionId)
        {
            return await _context.Notifications.AsNoTracking()
                .Where(n => n.subscripcionId == subscriptionId)
                .OrderByDescending(n => n.createAt)
                .ToListAsync();
        }
    }
}
