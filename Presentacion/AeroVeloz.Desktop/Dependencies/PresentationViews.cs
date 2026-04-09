using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Views;
using AeroVeloz.Desktop.Views.SuperAdmin;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;
using AeroVeloz.Desktop.ViewModels.AirportAdmin;
using AeroVeloz.Desktop.Views.AirportAdmin;

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

        services.AddTransient<AdminDetailView>(provider =>
        {
            var view = new AdminDetailView();
            view.DataContext = provider.GetRequiredService<AdminDetailViewModel>();
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

                services.AddTransient<AirportAdminMainView>(provider =>
        {
            var view = new AirportAdminMainView();
            view.DataContext = provider.GetRequiredService<AirportAdminMainViewModel>();
            return view;
        });
        services.AddTransient<AirportAdminDashboardView>(provider =>
        {
            var view = new AirportAdminDashboardView();
            view.DataContext = provider.GetRequiredService<AirportAdminDashboardViewModel>();
            return view;
        });
        services.AddTransient<UserListView>(provider =>
        {
            var view = new UserListView();
            view.DataContext = provider.GetRequiredService<UserListViewModel>();
            return view;
        });
        services.AddTransient<UserDetailView>(provider =>
        {
            var view = new UserDetailView();
            view.DataContext = provider.GetRequiredService<UserDetailViewModel>();
            return view;
        });
        services.AddTransient<OperationsListView>(provider =>
        {
            var view = new OperationsListView();
            view.DataContext = provider.GetRequiredService<OperationsListViewModel>();
            return view;
        });
        services.AddTransient<OperationDetailView>(provider =>
        {
            var view = new OperationDetailView();
            view.DataContext = provider.GetRequiredService<OperationDetailViewModel>();
            return view;
        });
        services.AddTransient<ConnectionListView>(provider =>
        {
            var view = new ConnectionListView();
            view.DataContext = provider.GetRequiredService<ConnectionListViewModel>();
            return view;
        });
        services.AddTransient<AuditLogView>(provider =>
        {
            var view = new AuditLogView();
            view.DataContext = provider.GetRequiredService<AuditLogViewModel>();
            return view;
        });
        return services;
    }
}

