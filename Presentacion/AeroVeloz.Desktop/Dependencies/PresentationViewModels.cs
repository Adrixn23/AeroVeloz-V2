using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.ViewModels;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;

namespace AeroVeloz.Desktop.Dependencies;

public static class PresentationViewModels
{
    public static IServiceCollection AddPresentationViewModels(this IServiceCollection services)
    {
        services.AddTransient<LoginViewModel>();
        services.AddTransient<SuperAdminDashboardViewModel>();
        services.AddTransient<SuperAdminMainViewModel>();
        services.AddTransient<AdminListViewModel>();
        services.AddTransient<AirportListViewModel>();
        services.AddTransient<AirportDetailViewModel>();

        return services;
    }
}
