using AeroVeloz.Domain.Entities.Users.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.HasData(
                new Roles { Id = 3, nameRol = "AIRLINEADMIN" }
            );
        }
    }
}
