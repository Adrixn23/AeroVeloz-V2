using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using AeroVeloz.Desktop.Dependencies;
using AeroVeloz.Desktop.Views;

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
                       
                        services.AddPresentationHttpServices(context.Configuration);
                        services.AddPresentationCoreServices();
                        services.AddPresentationApiServices(context.Configuration);
                        services.AddPresentationViewModels();
                        services.AddPresentationViews();
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
