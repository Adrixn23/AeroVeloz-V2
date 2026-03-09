using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations;

public class FlightStateConfiguration : IEntityTypeConfiguration<FlightState>
{
    public void Configure(EntityTypeBuilder<FlightState> builder)
    {
        builder.ToTable("FlightStates");

        builder.HasKey(fs => fs.Id);

        builder.Property(fs => fs.Id)
            .HasColumnName("StateID")
            .HasColumnType("tinyint")
            .ValueGeneratedNever();

        builder.Property(fs => fs.code)
            .HasColumnName("code")
            .HasMaxLength(50);

        builder.Property(fs => fs.StateName)
            .HasColumnName("StateName")
            .HasMaxLength(100);

        builder.HasData(
            new FlightState { Id = (byte)FlightStateEnum.Scheduled, code = "Scheduled", StateName = "Scheduled" },
            new FlightState { Id = (byte)FlightStateEnum.InProgress, code = "InProgress", StateName = "In Progress" },
            new FlightState { Id = (byte)FlightStateEnum.Delayed, code = "Delayed", StateName = "Delayed" },
            new FlightState { Id = (byte)FlightStateEnum.InFlight, code = "InFlight", StateName = "In Flight" },
            new FlightState { Id = (byte)FlightStateEnum.Landed, code = "Landed", StateName = "Landed/Arrived" },
            new FlightState { Id = (byte)FlightStateEnum.Finished, code = "Finished", StateName = "Finished" },
            new FlightState { Id = (byte)FlightStateEnum.Cancelled, code = "Cancelled", StateName = "Cancelled" },
            new FlightState { Id = (byte)FlightStateEnum.Diverted, code = "Diverted", StateName = "Diverted" }
        );
    }
}
