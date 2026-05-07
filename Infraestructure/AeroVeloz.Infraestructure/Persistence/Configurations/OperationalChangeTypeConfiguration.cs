using AeroVeloz.Domain.Entities.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class OperationalChangeTypeConfiguration : IEntityTypeConfiguration<OperationalChangeType>
    {
        public void Configure(EntityTypeBuilder<OperationalChangeType> builder)
        {
            builder.HasData(
                new OperationalChangeType { Id = 1, name = "GATE_CHANGE" },
                new OperationalChangeType { Id = 2, name = "FLIGHT_DELAY" },
                new OperationalChangeType { Id = 3, name = "FLIGHT_CANCELLATION" }
            );
        }
    }
}
