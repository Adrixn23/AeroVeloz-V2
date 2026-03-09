using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.Repositories.Notifications;
using AeroVeloz.Infraestructure.Integrations.Channels;
using AeroVeloz.Infraestructure.Integrations.Notifications;
using AeroVeloz.Infraestructure.Integrations.OneSignal;
using AeroVeloz.Infraestructure.Persistence.Repositories.Notifications;
using AeroVeloz.Infraestructure.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class NotificationDependencies
    {
        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationDispatcher, NotificationDispatcher>();

            services.AddScoped<INotificationChannel, EmailNotificationChannel>();
            services.AddScoped<INotificationChannel, SmsNotificationChannel>();
            services.AddScoped<INotificationChannel, OneSignalPushChannel>();
            services.AddScoped<INotificationChannel, OneSignalInAppChannel>();

            services.AddScoped<ICsvFlightParser, CsvFlightParser>();

            return services;
        }
    }
}
