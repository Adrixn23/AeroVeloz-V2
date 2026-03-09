using AeroVeloz.Infraestructure.Integrations.Notifications;
using AeroVeloz.Infraestructure.Integrations.OneSignal;
using AeroVeloz.Transversal.Contracts.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class NotificationDependencies
    {
        public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OneSignalOptions>(options =>
            {
                options.AppId = configuration["OneSignal:AppId"] ?? string.Empty;
                options.RestApiKey = configuration["OneSignal:RestApiKey"] ?? string.Empty;
            });

            services.AddHttpClient<OneSignalPushChannel>();
            services.AddHttpClient<OneSignalInAppChannel>();

            services.AddSingleton<INotificationChannel, OneSignalPushChannel>();
            services.AddSingleton<INotificationChannel, OneSignalInAppChannel>();
            services.AddSingleton<INotificationDispatcher, NotificationDispatcher>();

            return services;
        }
    }
}
