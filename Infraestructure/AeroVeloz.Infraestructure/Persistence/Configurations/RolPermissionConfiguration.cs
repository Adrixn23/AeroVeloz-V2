using AeroVeloz.Domain.Entities.Users.RolesPermision;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class RolPermissionConfiguration : IEntityTypeConfiguration<RolPermission>
    {
        public void Configure(EntityTypeBuilder<RolPermission> builder)
        {
            builder.HasData(
                new RolPermission { Id = 1, idRol = 3, idPermission = 1 },  // FLIGHT_UPLOAD_BATCH
                new RolPermission { Id = 2, idRol = 3, idPermission = 2 },  // FLIGHT_UPDATE_STATE
                new RolPermission { Id = 3, idRol = 3, idPermission = 3 },  // FLIGHT_VIEW_OWN
                new RolPermission { Id = 4, idRol = 3, idPermission = 4 },  // FLIGHT_VIEW_SUBSCRIPTIONS
                new RolPermission { Id = 5, idRol = 3, idPermission = 5 },  // CONNECTION_REQUEST
                new RolPermission { Id = 6, idRol = 3, idPermission = 6 }   // CONNECTION_VIEW
            );
        }
    }
}
