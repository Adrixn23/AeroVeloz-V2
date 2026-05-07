using AeroVeloz.Domain.Entities.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class ChannelSubscriptionNotificationConfiguration : IEntityTypeConfiguration<ChannelSubscriptionNotification>
    {
        public void Configure(EntityTypeBuilder<ChannelSubscriptionNotification> builder)
        {
            builder.HasData(
                new ChannelSubscriptionNotification { Id = 1, name = "Email" },
                new ChannelSubscriptionNotification { Id = 2, name = "SMS" },
                new ChannelSubscriptionNotification { Id = 3, name = "Push" }
            );
        }
    }
}
