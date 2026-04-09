using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Services.Implementations.AdminSystem;
using AeroVeloz.Desktop.Services.Implementations.Airport;
using AeroVeloz.Desktop.Services.Implementations.Audit;
using AeroVeloz.Desktop.Services.Implementations.Users;
using AeroVeloz.Desktop.Services.Interfaces.AdminSystem;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.Services.Interfaces.Users;
using AeroVeloz.Desktop.Services.Interfaces.Audit;
namespace AeroVeloz.Desktop.Dependencies;

public static class PresentationApiServices
{
    public static IServiceCollection AddPresentationApiServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddTransient<IAirportService, AirportService>();
        services.AddTransient<ISuperAdminStatService, SuperAdminStatService>();
        services.AddTransient<IAirportAdminStatService, AirportAdminStatService>();
        services.AddTransient<IAdminManagerService, AdminManagerService>();
        services.AddTransient<IAuditService, AuditService>();
        services.AddTransient<IManagerUserService, ManagerUserService>();
        services.AddTransient<IOperationService, OperationService>();
        services.AddTransient<IAirportConnectionService, AirportConnectionService>();

        return services;
    }
}

