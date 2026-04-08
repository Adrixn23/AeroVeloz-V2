using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Services.Implementations;
using AeroVeloz.Desktop.Services.Interfaces;
namespace AeroVeloz.Desktop.Dependencies;

public static class PresentationApiServices
{
    public static IServiceCollection AddPresentationApiServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddTransient<IAirportService, AirportService>();
        services.AddTransient<ISuperAdminStatService, SuperAdminStatService>();
        services.AddTransient<IAdminManagerService, AdminManagerService>();

        return services;
    }
}
