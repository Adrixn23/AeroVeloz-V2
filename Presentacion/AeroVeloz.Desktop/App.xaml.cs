using System;
using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AeroVeloz.Desktop.Services.Implementations;
using AeroVeloz.Desktop.Services.Interfaces;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Http;
using AeroVeloz.Desktop.ViewModels;
using AeroVeloz.Desktop.ViewModels.SuperAdmin;
using AeroVeloz.Desktop.Views.SuperAdmin;
using AeroVeloz.Desktop.Views;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.Desktop
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            try
            {
                AppHost = Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        config.SetBasePath(AppContext.BaseDirectory);
                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    })
                    .ConfigureServices((context, services) =>
                    {
                        var baseUrl = context.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7126";
                        services.AddTransient<HttpErrorInterceptorHandler>(provider => new HttpErrorInterceptorHandler(provider.GetRequiredService<IDialogService>()));

                        services.AddHttpClient("AeroVelozApi", client =>
                        {
                            client.BaseAddress = new Uri(baseUrl);
                        })
                        .AddHttpMessageHandler<HttpErrorInterceptorHandler>();

                        services.AddSingleton<IAuthService, AuthService>();
                        services.AddSingleton<IDialogService, DialogService>();
                        services.AddSingleton<ISessionService, SessionService>();
                        services.AddTransient<IAirportService, AirportService>();
                        services.AddTransient<ISuperAdminStatService, SuperAdminStatService>();
                        services.AddTransient<IAdminManagerService, AdminManagerService>();

                        services.AddTransient<LoginViewModel>();
                        services.AddTransient<LoginView>();
                        services.AddTransient<SuperAdminDashboardViewModel>();
                        services.AddTransient<AdminListViewModel>();
                        services.AddTransient<AirportListViewModel>();
                        services.AddTransient<AirportDetailViewModel>();
                        services.AddTransient<SuperAdminDashboardView>(provider => 
                        {
                            var view = new SuperAdminDashboardView();
                            view.DataContext = provider.GetRequiredService<SuperAdminDashboardViewModel>();
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

                        services.AddTransient<SuperAdminMainViewModel>();
                        services.AddTransient<SuperAdminMainView>(provider => 
                        {
                            var view = new SuperAdminMainView();
                            view.DataContext = provider.GetRequiredService<SuperAdminMainViewModel>();
                            return view;
                        });
                    })
                    .Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar la aplicación (Configuración DI): {ex.Message}", "Error Crítico", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (AppHost != null)
                {
                    await AppHost.StartAsync();

                    var startupForm = AppHost.Services.GetRequiredService<LoginView>();
                    startupForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la vista de Login: {ex.Message}\n\n{ex.StackTrace}", "Error Crítico", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (AppHost != null)
            {
                await AppHost.StopAsync();
            }
            base.OnExit(e);
        }
    }
}
