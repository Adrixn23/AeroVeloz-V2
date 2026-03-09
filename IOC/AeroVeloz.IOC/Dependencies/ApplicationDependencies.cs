using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.Contracts.Audit;
using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.Contracts.Operations;
using AeroVeloz.Application.Contracts.Users;
using AeroVeloz.Application.Handlers.Airport;
using AeroVeloz.Application.Handlers.Audit;
using AeroVeloz.Application.Handlers.Auth;
using AeroVeloz.Application.Handlers.Operations;
using AeroVeloz.Application.Handlers.Users;
using Microsoft.Extensions.DependencyInjection;

namespace AeroVeloz.IOC.Dependencies
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAirportHandler, AirportHandler>();
            services.AddScoped<IAirportConnectionHandler, AirportConnectionHandler>();
            services.AddScoped<IUserHandler, UserHandler>();
            services.AddScoped<IOperationalHandler, OperationalHandler>();
            services.AddScoped<IAuthenticationHandler, AuthenticationHandler>();
            services.AddScoped<IAuditHandler, AuditHandler>();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AirportHandler).Assembly));

            return services;
        }
    }
}
