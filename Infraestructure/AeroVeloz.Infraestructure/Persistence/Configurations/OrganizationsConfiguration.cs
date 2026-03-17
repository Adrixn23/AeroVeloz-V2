using AeroVeloz.Domain.Entities.Organization.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class OrganizationsConfiguration : IEntityTypeConfiguration<Organizations>
    {
        public void Configure(EntityTypeBuilder<Organizations> builder)
        {
            builder.HasData(
                new Organizations
                {
                    Id = 1,
                    nameOrganization = "aerovelozGlobal",
                    typeOrganization = "admin",
                    isActived = true,
                    emailOrganization = "Admin@Aeroveloz.com",
                    createAt = DateTime.Now // Using DateTime.Now to approximate getDate() for seed
                }
            );
        }
    }
}
