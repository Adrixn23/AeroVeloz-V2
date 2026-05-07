using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Infraestructure.Integrations.Channels;
using AeroVeloz.Infraestructure.Integrations.Notifications;
using AeroVeloz.Infraestructure.Integrations.OneSignal;
using AeroVeloz.Infraestructure.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace AeroVeloz.IOC.Dependencies
{
    public static class NotificationDependencies
    {
        public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(); // Esto registra el servicio básico de HttpClient
            
            services.AddScoped<INotificationDispatcher, NotificationDispatcher>();

            // Canales de notificación
            services.AddHttpClient<OneSignalPushChannel>();
            services.AddHttpClient<OneSignalInAppChannel>();
            
            services.AddScoped<INotificationChannel, EmailNotificationChannel>();
            services.AddScoped<INotificationChannel, SmsNotificationChannel>();
            services.AddScoped<INotificationChannel>(sp => sp.GetRequiredService<OneSignalPushChannel>());
            services.AddScoped<INotificationChannel>(sp => sp.GetRequiredService<OneSignalInAppChannel>());

            // Configuración de OneSignal (si se necesita)
            services.Configure<OneSignalOptions>(configuration.GetSection("OneSignal"));

            // Servicios de parsing (CSV)
            services.AddScoped<ICsvFlightParser, CsvFlightParser>();

            return services;
        }
    }
}