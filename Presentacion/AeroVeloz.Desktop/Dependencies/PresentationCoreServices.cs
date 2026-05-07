using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Services.Dialog;
using AeroVeloz.Desktop.Services.Implementations.Airport;
using AeroVeloz.Desktop.Services.Implementations.Audit;
using AeroVeloz.Desktop.Services.Implementations.Auth;
using AeroVeloz.Desktop.Services.Implementations.Notifications;
using AeroVeloz.Desktop.Services.Implementations.Users;
using AeroVeloz.Desktop.Services.Interfaces.Airport;
using AeroVeloz.Desktop.Services.Interfaces.Auth;
using AeroVeloz.Desktop.Services.Interfaces.Users;
using AeroVeloz.Desktop.Services.Interfaces.Audit;

namespace AeroVeloz.Desktop.Dependencies;

public static class PresentationCoreServices
{
    public static IServiceCollection AddPresentationCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<ISessionService, SessionService>();
        services.AddSingleton<NotificationService>();

        services.AddTransient<IAirportAdminStatService, AirportAdminStatService>();
        services.AddTransient<IManagerUserService, ManagerUserService>();
        services.AddTransient<IOperationService, OperationService>();
        services.AddTransient<IAirportConnectionService, AirportConnectionService>();
        services.AddTransient<IAuditService, AuditService>();

        return services;
    }
}
