using AeroVeloz.Infraestructure.Integrations.Notifications;
using AeroVeloz.Infraestructure.Integrations.OneSignal;
using AeroVeloz.Infraestructure.Integrations.Email;
using AeroVeloz.Application.Repositories.Notifications;
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

            services.Configure<SmtpEmailOptions>(options =>
            {
                options.Host = configuration["Smtp:Host"] ?? string.Empty;
                options.Port = int.TryParse(configuration["Smtp:Port"], out var port) ? port : 587;
                options.UserName = configuration["Smtp:UserName"] ?? string.Empty;
                options.Password = configuration["Smtp:Password"] ?? string.Empty;
                options.FromAddress = configuration["Smtp:FromAddress"] ?? string.Empty;
                options.FromName = configuration["Smtp:FromName"] ?? "AeroVeloz";
                options.EnableSsl = bool.TryParse(configuration["Smtp:EnableSsl"], out var ssl) ? ssl : true;
            });

            services.AddHttpClient<OneSignalPushChannel>();
            services.AddHttpClient<OneSignalInAppChannel>();

            services.AddSingleton<INotificationChannel, OneSignalPushChannel>();
            services.AddSingleton<INotificationChannel, OneSignalInAppChannel>();
            services.AddSingleton<INotificationChannel, SmtpEmailChannel>();
            services.AddSingleton<INotificationDispatcher, NotificationDispatcher>();

            return services;
        }
    }
}
