using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.ViewModels;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;
using AeroVeloz.Desktop.ViewModels.OperationalUser;

namespace AeroVeloz.Desktop.Dependencies;

public static class PresentationViewModels
{
    public static IServiceCollection AddPresentationViewModels(this IServiceCollection services)
    {
        services.AddTransient<LoginViewModel>();
        services.AddTransient<SuperAdminDashboardViewModel>();
        services.AddTransient<SuperAdminMainViewModel>();
        services.AddTransient<AdminListViewModel>();
        services.AddTransient<AdminDetailViewModel>();
        services.AddTransient<AeroVeloz.Desktop.ViewModels.SuperAdmin.AirlineListViewModel>();
        services.AddTransient<AeroVeloz.Desktop.ViewModels.AirportAdmin.AirlineListViewModel>();
        services.AddTransient<AeroVeloz.Desktop.ViewModels.OperationalUser.AirlineListViewModel>();
        services.AddTransient<AirportListViewModel>();
        services.AddTransient<AirportDetailViewModel>();

        // Airport Admin ViewModels
        services.AddTransient<AirportAdminDashboardViewModel>();
        services.AddTransient<AirportAdminMainViewModel>();
        services.AddTransient<UserListViewModel>();
        services.AddTransient<UserDetailViewModel>();
        services.AddTransient<OperationsListViewModel>();
        services.AddTransient<OperationDetailViewModel>();
        services.AddTransient<ConnectionListViewModel>();
        services.AddTransient<ConnectionDetailViewModel>();
        services.AddTransient<AuditLogViewModel>();

        // Operational User ViewModels
        services.AddTransient<OperationalUserMainViewModel>();
        services.AddTransient<OperationalUserDashboardViewModel>();

        return services;
    }
}

