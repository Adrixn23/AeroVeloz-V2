using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Views;
using AeroVeloz.Desktop.Views.SuperAdmin;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;

namespace AeroVeloz.Desktop.Dependencies;

public static class PresentationViews
{
    public static IServiceCollection AddPresentationViews(this IServiceCollection services)
    {
        
        services.AddTransient<LoginView>();

        services.AddTransient<SuperAdminDashboardView>(provider =>
        {
            var view = new SuperAdminDashboardView();
            view.DataContext = provider.GetRequiredService<SuperAdminDashboardViewModel>();
            return view;
        });

        services.AddTransient<SuperAdminMainView>(provider =>
        {
            var view = new SuperAdminMainView();
            view.DataContext = provider.GetRequiredService<SuperAdminMainViewModel>();
            return view;
        });

        services.AddTransient<AdminListView>(provider =>
        {
            var view = new AdminListView();
            view.DataContext = provider.GetRequiredService<AdminListViewModel>();
            return view;
        });

        services.AddTransient<AirportListView>(provider =>
        {
            var view = new AirportListView();
            view.DataContext = provider.GetRequiredService<AirportListViewModel>();
            return view;
        });

        services.AddTransient<AirportDetailView>(provider =>
        {
            var view = new AirportDetailView();
            view.DataContext = provider.GetRequiredService<AirportDetailViewModel>();
            return view;
        });

        return services;
    }
}
