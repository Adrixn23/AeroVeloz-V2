using Microsoft.Extensions.DependencyInjection;
using AeroVeloz.Desktop.Services.Implementations;
using AeroVeloz.Desktop.Services.Interfaces;
using AeroVeloz.Desktop.Services.Dialog;

namespace AeroVeloz.Desktop.Dependencies;

public static class PresentationCoreServices
{
    public static IServiceCollection AddPresentationCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<ISessionService, SessionService>();

        services.AddTransient<IAirportAdminStatService, AirportAdminStatService>();
        services.AddTransient<IManagerUserService, ManagerUserService>();
        services.AddTransient<IOperationService, OperationService>();
        services.AddTransient<IAirportConnectionService, AirportConnectionService>();
        services.AddTransient<IAuditService, AuditService>();

        return services;
    }
}
