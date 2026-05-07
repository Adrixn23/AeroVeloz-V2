using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using AeroVeloz.Infraestructure.Persistence.context;
using System.IO;

namespace AeroVeloz.Infraestructure.Persistence.Context;

public class AeroVelozContextFactory : IDesignTimeDbContextFactory<AeroVelozContext>
{
    public AeroVelozContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AeroVelozContext>();

        // Default connection string if not provided in environment or appsettings
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aerovelozdb;Trusted_Connection=True;MultipleActiveResultSets=true")
            .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));

        return new AeroVelozContext(optionsBuilder.Options);
    }
}
