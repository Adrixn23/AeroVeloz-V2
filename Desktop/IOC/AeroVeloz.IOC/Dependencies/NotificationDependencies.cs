using AeroVeloz.Infraestructure.Integrations.Notifications;
using AeroVeloz.Infraestructure.Integrations.Email;
using AeroVeloz.Application.Repositories.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Infraestructure.Integrations.Notifications.SignalR;

namespace AeroVeloz.IOC.Dependencies
{
    public static class NotificationDependencies
    {
        public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
        {
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

            services.AddSignalR();

            services.AddSingleton<INotificationChannel>(sp => 
                new SignalRNotificationChannel(sp.GetRequiredService<Microsoft.AspNetCore.SignalR.IHubContext<NotificationHub>>(), AeroVeloz.Domain.Common.Notification.ChannelType.Push));
            
            services.AddSingleton<INotificationChannel>(sp => 
                new SignalRNotificationChannel(sp.GetRequiredService<Microsoft.AspNetCore.SignalR.IHubContext<NotificationHub>>(), AeroVeloz.Domain.Common.Notification.ChannelType.InApp));
                
            services.AddSingleton<INotificationChannel, SmtpEmailChannel>();
            services.AddSingleton<INotificationDispatcher, NotificationDispatcher>();

            return services;
        }
    }
}
