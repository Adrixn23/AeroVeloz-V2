using AeroVeloz.Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class ProviderResponseConfiguration : IEntityTypeConfiguration<ProviderResponse>
    {
        public void Configure(EntityTypeBuilder<ProviderResponse> builder)
        {
            builder.HasData(
                new ProviderResponse { Id = 1, name = "SMS" },
                new ProviderResponse { Id = 2, name = "Email" },
                new ProviderResponse { Id = 3, name = "Push Notification" }
            );
        }
    }
}
