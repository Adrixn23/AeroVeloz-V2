using AeroVeloz.Domain.Entities.Users.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                new Permission { Id = 1, codePermision = "FLIGHT_UPLOAD_BATCH", description = "Upload flight batch via CSV" },
                new Permission { Id = 2, codePermision = "FLIGHT_UPDATE_STATE", description = "Update flight state" },
                new Permission { Id = 3, codePermision = "FLIGHT_VIEW_OWN", description = "View own airline flights" },
                new Permission { Id = 4, codePermision = "FLIGHT_VIEW_SUBSCRIPTIONS", description = "View flight subscription count" },
                new Permission { Id = 5, codePermision = "CONNECTION_REQUEST", description = "Request airport connection" },
                new Permission { Id = 6, codePermision = "CONNECTION_VIEW", description = "View airline connections" }
            );
        }
    }
}
