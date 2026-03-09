using AeroVeloz.Domain.Entities.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class AuditTypeConfiguration : IEntityTypeConfiguration<AuditType>
    {
        public void Configure(EntityTypeBuilder<AuditType> builder)
        {
            builder.HasData(
                new AuditType { Id = 1, nameAudit = "ENTITY_CREATE" },
                new AuditType { Id = 2, nameAudit = "ENTITY_UPDATE" },
                new AuditType { Id = 3, nameAudit = "ENTITY_DEACTIVATE" }
            );
        }
    }
}
